using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Messaging;
using System.Threading.Tasks;

namespace RentChallenge.Application.Services.Registrations
{
    // Serviço responsável por publicar solicitações de registro de aluguel (Rental) na fila de mensageria.
    public class RentalRegistrationService(
        IMessagePublisher messagePublisher
    ) : IRentalRegistrationService
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        // Publica a entidade Rental no tópico "register-rental".
        public async Task EnqueueRegistrationAsync(Rental rental) =>
            await _messagePublisher.PublishAsync("register-rental", rental);
    }

}
