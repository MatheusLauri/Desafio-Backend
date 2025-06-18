using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Messaging;
using System.Threading.Tasks;

namespace RentChallenge.Application.Services.Registrations
{
    // Servi�o respons�vel por publicar solicita��es de registro de aluguel (Rental) na fila de mensageria.
    public class RentalRegistrationService(
        IMessagePublisher messagePublisher
    ) : IRentalRegistrationService
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        // Publica a entidade Rental no t�pico "register-rental".
        public async Task EnqueueRegistrationAsync(Rental rental) =>
            await _messagePublisher.PublishAsync("register-rental", rental);
    }

}
