using AutoMapper;
using FluentValidation;
using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Application.Interfaces.APIs;
using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Exceptions;
using RentChallenge.Domain.Interfaces.Repositories;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Text.Json;

namespace RentChallenge.Application.Services.APIs
{
    // Serviço responsável por encapsular a lógica de negócio relacionada às motos (motorcycles).
    // Implementa a interface IMotorcycleApiService e injeta dependências via construtor primário.
    public class MotorcycleApiService(
        IValidator<RegisterMotorcycleRequestDTO> validator,
        IMotorcycleRegistrationService registrationService,
        IMotorcycleRepository repository,
        IEventMotorcycleRegistrationService eventMotorcycleRegistrationService,
        IMapper mapper
    ) : IMotorcycleApiService
    {
        private readonly IValidator<RegisterMotorcycleRequestDTO> _validator = validator;
        private readonly IMotorcycleRegistrationService _registrationService = registrationService;
        private readonly IEventMotorcycleRegistrationService _eventMotorcycleRegistrationService = eventMotorcycleRegistrationService;
        private readonly IMotorcycleRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        // Realiza o registro de uma moto, validando os dados e garantindo que não haja duplicidade.
        // Se o ano for >= 2024, também publica um evento no sistema assíncrono.
        public async Task RegisterAsync(RegisterMotorcycleRequestDTO motorcycle)
        {
            var result = await _validator.ValidateAsync(motorcycle);

            if (!result.IsValid || await MotorcycleExists(motorcycle))
                throw new ValidationException(result.Errors);

            await _registrationService.EnqueueRegistrationAsync(motorcycle);

            if (motorcycle.Year >= 2024)
                await _eventMotorcycleRegistrationService
                    .EnqueueRegistrationAsync(_mapper.Map<EventMotorcycleRegistered>(motorcycle));
        }

        // Retorna todas as motos registradas, podendo filtrar por placa (case-insensitive).
        public async Task<List<Motorcycle>> GetAllAsync(string? numberPlate)
        {
            var motorcycles = await _repository.GetAllAsync();

            if (!string.IsNullOrEmpty(numberPlate))
                motorcycles = motorcycles
                    .Where(m => m.NumberPlate.Contains(numberPlate, StringComparison.OrdinalIgnoreCase));

            return motorcycles.ToList();
        }

        // Atualiza a placa de uma moto a partir do identificador.
        public async Task UpdateNumberPlateAsync(string identifier, string numberPlate)
        {
            var motorcycle = await GetMotorcycleByIdentifierAsync(identifier);
            motorcycle.NumberPlate = numberPlate;
            await _repository.UpdateAsync(motorcycle);
        }

        // Retorna uma moto específica por identificador.
        public async Task<Motorcycle> GetByIdentifierAsync(string identifier) =>
            await GetMotorcycleByIdentifierAsync(identifier);

        // Remove uma moto do sistema com base no identificador.
        public async Task DeleteAsync(string identifier)
        {
            var motorcycle = await GetMotorcycleByIdentifierAsync(identifier);
            await _repository.DeleteAsync(motorcycle);
        }

        // Busca uma moto pelo identificador e lança exceção se não existir.
        private async Task<Motorcycle> GetMotorcycleByIdentifierAsync(string identifier)
        {
            var motorcycle = await _repository.GetByIdentifierAsync(identifier);
            if (motorcycle is null)
                throw new NotFoundException($"Moto não encontrada.");
            return motorcycle;
        }

        // Verifica se já existe uma moto com a mesma placa ou identificador (case-insensitive).
        private async Task<bool> MotorcycleExists(RegisterMotorcycleRequestDTO motorcycle)
        {
            var existingMotorcycles = await _repository.GetAllAsync();
            return existingMotorcycles.Any(m =>
                m.NumberPlate.Equals(motorcycle.NumberPlate, StringComparison.OrdinalIgnoreCase) ||
                m.Identifier.Equals(motorcycle.Identifier, StringComparison.OrdinalIgnoreCase));
        }
    }
}