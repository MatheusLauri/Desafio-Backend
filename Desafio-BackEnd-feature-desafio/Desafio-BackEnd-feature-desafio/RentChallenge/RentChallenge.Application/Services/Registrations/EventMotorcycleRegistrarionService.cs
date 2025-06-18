using RentChallenge.Application.DTOs.Requests;
using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Messaging;

namespace RentChallenge.Application.Services.Registrations
{
    // Serviço responsável por publicar eventos relacionados ao registro de motos.
    // Implementa a interface IEventMotorcycleRegistrationService e usa um publisher de mensagens para envio assíncrono.
    public class EventMotorcycleRegistrationService(
        IMessagePublisher messagePublisher
    ) : IEventMotorcycleRegistrationService
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        // Publica o evento EventMotorcycleRegistered no tópico "register-motorcycle-event".
        public async Task EnqueueRegistrationAsync(EventMotorcycleRegistered eventMotorcycle) =>
            await _messagePublisher.PublishAsync("register-motorcycle-event", eventMotorcycle);
    }

}
