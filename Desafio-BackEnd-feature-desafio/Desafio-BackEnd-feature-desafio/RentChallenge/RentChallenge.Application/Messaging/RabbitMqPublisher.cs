// Importa a interface do RabbitMQ.Client e a interface de mensageria do domínio.
// Também importa utilitários de codificação de texto e serialização JSON.
using RabbitMQ.Client;
using RentChallenge.Domain.Interfaces.Messaging;
using System.Text;
using System.Text.Json;

namespace RentChallenge.Application.Messaging
{
    // Implementa a interface IMessagePublisher, que define o contrato para publicação de mensagens.
    public class RabbitMqPublisher : IMessagePublisher
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        // Construtor que injeta uma fábrica de conexões RabbitMQ.
        // Cria a conexão e o canal de forma assíncrona, porém usando `.Result` (sincronamente).
        public RabbitMqPublisher(IRabbitMqConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.CreateConnection().Result;
            _channel = _connection.CreateChannelAsync().Result;
        }

        // Método responsável por publicar uma mensagem em uma fila específica.
        public async Task PublishAsync(string topic, object message)
        {
            // Serializa o objeto da mensagem em JSON e codifica como bytes UTF-8.
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            // Garante que a fila existe antes de publicar (cria se não existir).
            await _channel.QueueDeclareAsync(
                queue: topic,
                durable: true,      // Mantém a fila mesmo se o RabbitMQ for reiniciado
                exclusive: false,   // Permite que várias conexões usem essa fila
                autoDelete: false   // Não remove a fila automaticamente quando o último consumidor desconectar
            );

            // Define propriedades básicas da mensagem (por enquanto só o content type).
            var basicProperties = new BasicProperties
            {
                ContentType = "application/json"
            };

            // Publica a mensagem no canal, usando a fila como routing key.
            await _channel.BasicPublishAsync(
                exchange: "",               // Fila direta (sem exchange custom)
                routingKey: topic,          // Nome da fila (também atua como routing key)
                mandatory: false,
                basicProperties: basicProperties,
                body: body
            );
        }

        // Libera os recursos alocados: canal e conexão com o RabbitMQ.
        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
