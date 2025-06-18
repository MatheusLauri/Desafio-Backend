using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Application.Interfaces.Handlers;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Messaging;
using System.Text;
using System.Text.Json;

namespace RentChallenge.Consumer
{
    public class RabbitMqListener
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public RabbitMqListener(IServiceProvider serviceProvider, IRabbitMqConnectionFactory connection)
        {
            _serviceProvider = serviceProvider;
            _connection = connection.CreateConnection().Result;
            _channel = _connection.CreateChannelAsync().Result;
        }

        public async Task Listen()
        {
            await RegisterQueue<RegisterMotorcycleRequestDTO>("register-motorcycle", async (msg) =>
            {
                using var scope = _serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<IMotorcycleHandlerService>();
                await handler.HandleAsync(msg);
            });

            await RegisterQueue<EventMotorcycleRegistered>("register-motorcycle-event", async (msg) =>
            {
                using var scope = _serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<IEventMotorcycleHandlerService>();
                await handler.HandleAsync(msg);
            });

            await RegisterQueue<DeliveryMan>("register-deliveryman", async (msg) =>
            {
                using var scope = _serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<IDeliveryManHandlerService>();
                await handler.HandleAsync(msg);
            });

            await RegisterQueue<Rental>("register-rental", async (msg) =>
            {
                using var scope = _serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<IRentalHandlerService>();
                await handler.HandleAsync(msg);
            });
        }

        private async Task RegisterQueue<T>(string queue, Func<T, Task> handler)
        {
            await _channel.ExchangeDeclareAsync(exchange: queue, type: ExchangeType.Fanout, durable: true);
            await _channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false);
            await _channel.QueueBindAsync(queue: queue, exchange: queue, routingKey: "");
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body));
                if (message != null)
                {
                    await handler(message);
                }
            };

            await _channel.BasicConsumeAsync(queue: queue, autoAck: true, consumer: consumer);
        }
    }
}
