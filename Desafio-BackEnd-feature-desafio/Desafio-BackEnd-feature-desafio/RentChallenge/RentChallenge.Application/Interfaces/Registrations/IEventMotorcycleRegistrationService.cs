using RentChallenge.Application.DTOs.Requests;
using RentChallenge.Domain.Entities;

namespace RentChallenge.Application.Interfaces.Registrations
{
    public interface IEventMotorcycleRegistrationService
    {
        Task EnqueueRegistrationAsync(EventMotorcycleRegistered eventMotorcycle);
    }

}
