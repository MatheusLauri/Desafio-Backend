using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Messaging;

namespace RentChallenge.Application.Services.Registrations
{
    // Implementa a interface IMotorcycleRegistrationService e utiliza mensageria via IMessagePublisher.
    public class MotorcycleRegistrationService(
        IMessagePublisher messagePublisher
    ) : IMotorcycleRegistrationService
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        // Esse evento será processado por um handler que persistirá a moto no banco de dados.
        public async Task EnqueueRegistrationAsync(RegisterMotorcycleRequestDTO motorcycle) =>
            await _messagePublisher.PublishAsync("register-motorcycle", motorcycle);
    }

}
