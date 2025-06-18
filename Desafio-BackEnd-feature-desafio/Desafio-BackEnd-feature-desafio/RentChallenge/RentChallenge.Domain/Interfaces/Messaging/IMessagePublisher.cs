namespace RentChallenge.Domain.Interfaces.Messaging 
{
    public interface IMessagePublisher
    {
        Task PublishAsync(string topic, object message);
    }
}