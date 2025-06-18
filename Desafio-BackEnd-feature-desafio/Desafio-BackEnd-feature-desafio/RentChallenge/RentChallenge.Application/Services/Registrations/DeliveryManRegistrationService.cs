using RentChallenge.Application.DTOs.Requests.DeliveryMan;
using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Messaging;
using System.Threading.Tasks;

// Define o namespace da camada de aplicação responsável por serviços que realizam registro assíncrono (via fila/evento).
namespace RentChallenge.Application.Services.Registrations
{
    // Serviço responsável por enfileirar (publicar) uma entidade DeliveryMan para processamento assíncrono.
    // Implementa a interface IDeliveryManRegistrationService e injeta um publicador de mensagens.
    public class DeliveryManRegistrationService(
        IMessagePublisher messagePublisher
    ) : IDeliveryManRegistrationService
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        // Publica a entidade DeliveryMan no tópico "register-deliveryman" usando o mecanismo de mensageria.
        // Essa publicação geralmente será consumida por um handler que persistirá o dado posteriormente.
        public async Task EnqueueRegistrationAsync(DeliveryMan deliveryMan) =>
            await _messagePublisher.PublishAsync("register-deliveryman", deliveryMan);
    }
}
