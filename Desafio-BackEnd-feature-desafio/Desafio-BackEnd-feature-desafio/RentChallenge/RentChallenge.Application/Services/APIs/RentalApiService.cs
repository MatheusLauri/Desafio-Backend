using FluentValidation;
using RentChallenge.Application.Interfaces.APIs;
using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Define o namespace da camada de aplicação, onde ficam os serviços usados pela API.
namespace RentChallenge.Application.Services.APIs
{
    // Serviço responsável por lidar com a lógica da API de aluguéis (rentals).
    // Implementa a interface IRentalApiService e recebe as dependências via construtor primário (C# 12).
    public class RentalApiService(
        IValidator<Rental> validator,
        IRentalRegistrationService registrationService
    ) : IRentalApiService
    {
        private readonly IValidator<Rental> _validator = validator;
        private readonly IRentalRegistrationService _registrationService = registrationService;

        // Realiza o registro de um aluguel.
        // Primeiro valida a entidade Rental, e se for válida, envia para o fluxo assíncrono de registro.
        public async Task RegisterAsync(Rental rental)
        {
            var result = await _validator.ValidateAsync(rental);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            await _registrationService.EnqueueRegistrationAsync(rental);
        }
    }
}
