using AutoMapper;
using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Application.Interfaces.Handlers;
using RentChallenge.Application.Mappers;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Repositories;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// Define o namespace da camada de aplicação responsável por handlers, que executam ações como salvar dados após eventos ou comandos.
namespace RentChallenge.Application.Services.Handlers
{
    // Handler responsável por persistir uma entidade Motorcycle baseada em um DTO recebido (geralmente via fila).
    // Implementa a interface IMotorcycleHandlerService e injeta as dependências via construtor primário.
    public class MotorcycleHandlerService(
        IMotorcycleRepository motorcycleRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IMotorcycleHandlerService
    {
        private readonly IMotorcycleRepository _motorcycleRepository = motorcycleRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        // Manipula o DTO de moto, convertendo para a entidade Motorcycle e salvando no banco.
        public async Task HandleAsync(RegisterMotorcycleRequestDTO motorcycle)
        {
            await _motorcycleRepository.AddAsync(_mapper.Map<Motorcycle>(motorcycle));
            await _unitOfWork.CommitAsync();
        }
    }
}
