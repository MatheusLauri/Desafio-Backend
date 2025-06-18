using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Domain.Entities;

namespace RentChallenge.Application.Interfaces.APIs
{
    public interface IMotorcycleApiService
    {
        Task RegisterAsync(RegisterMotorcycleRequestDTO motorcycle);
        Task<List<Motorcycle>> GetAllAsync(string? numberPlate);
        Task UpdateNumberPlateAsync(string identifier, string numberPlate);
        Task<Motorcycle> GetByIdentifierAsync(string identifier);
        Task DeleteAsync(string identifier);

    }
}
