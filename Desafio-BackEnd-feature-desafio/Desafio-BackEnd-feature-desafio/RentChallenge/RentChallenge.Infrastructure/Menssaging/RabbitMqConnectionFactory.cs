using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RentChallenge.Domain.Interfaces.Messaging;

namespace RentChallenge.Infrastructure.Menssaging
{
    public class RabbitMqConnectionFactory(IConfiguration configuration) : IRabbitMqConnectionFactory
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<IConnection> CreateConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMq:Host"],
                UserName = _configuration["RabbitMq:User"],
                Password = _configuration["RabbitMq:Password"]
            };
            return await factory.CreateConnectionAsync();
        }

    }
}
