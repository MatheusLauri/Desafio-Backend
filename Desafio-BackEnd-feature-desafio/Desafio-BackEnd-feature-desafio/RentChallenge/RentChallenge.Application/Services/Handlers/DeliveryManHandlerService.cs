using RentChallenge.Application.Interfaces.Handlers;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Define o namespace da camada de aplicação, onde são implementados os manipuladores de eventos ou comandos.
namespace RentChallenge.Application.Services.Handlers
{
    // Serviço responsável por manipular a lógica de persistência do entregador após o processamento assíncrono.
    // Implementa a interface IDeliveryManHandlerService e injeta os repositórios e UnitOfWork via construtor primário.
    public class DeliveryManHandlerService(
        IDeliveryManRepository repository,
        IUnitOfWork unitOfWork
    ) : IDeliveryManHandlerService
    {
        public readonly IDeliveryManRepository _repository = repository;
        public readonly IUnitOfWork _unitOfWork = unitOfWork;

        // Manipula a entidade DeliveryMan, ajustando o DateTime se necessário e persistindo no banco de dados.
        public async Task HandleAsync(DeliveryMan deliveryMan)
        {
            if (deliveryMan.DateOfBirth.Kind == DateTimeKind.Unspecified)
                deliveryMan.DateOfBirth = DateTime.SpecifyKind(deliveryMan.DateOfBirth, DateTimeKind.Utc);

            await _repository.AddAsync(deliveryMan);
            await _unitOfWork.CommitAsync();
        }
    }
}
