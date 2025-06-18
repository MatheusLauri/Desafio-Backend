using RentChallenge.Application.DTOs.Requests.DeliveryMan;
using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Messaging;
using System.Threading.Tasks;

// Define o namespace da camada de aplica��o respons�vel por servi�os que realizam registro ass�ncrono (via fila/evento).
namespace RentChallenge.Application.Services.Registrations
{
    // Servi�o respons�vel por enfileirar (publicar) uma entidade DeliveryMan para processamento ass�ncrono.
    // Implementa a interface IDeliveryManRegistrationService e injeta um publicador de mensagens.
    public class DeliveryManRegistrationService(
        IMessagePublisher messagePublisher
    ) : IDeliveryManRegistrationService
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        // Publica a entidade DeliveryMan no t�pico "register-deliveryman" usando o mecanismo de mensageria.
        // Essa publica��o geralmente ser� consumida por um handler que persistir� o dado posteriormente.
        public async Task EnqueueRegistrationAsync(DeliveryMan deliveryMan) =>
            await _messagePublisher.PublishAsync("register-deliveryman", deliveryMan);
    }
}
