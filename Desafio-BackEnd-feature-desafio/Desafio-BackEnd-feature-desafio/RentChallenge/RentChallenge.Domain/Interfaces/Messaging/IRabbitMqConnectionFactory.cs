using RabbitMQ.Client;

namespace RentChallenge.Domain.Interfaces.Messaging
{
    public interface IRabbitMqConnectionFactory
    {
        Task<IConnection> CreateConnection();
    }
}
