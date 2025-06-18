using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Domain.Entities;

namespace RentChallenge.Application.Interfaces.Registrations
{
    public interface IMotorcycleRegistrationService
    {
        Task EnqueueRegistrationAsync(RegisterMotorcycleRequestDTO motorcycle);
    }

}
