using RentChallenge.Domain.Entities;

namespace RentChallenge.Domain.Interfaces.Repositories
{
    public interface IMotorcycleRepository
    {
        Task<Motorcycle?> GetByIdentifierAsync(string identifier);
        Task<IEnumerable<Motorcycle>> GetAllAsync();
        Task AddAsync(Motorcycle motorcycle);
        Task UpdateAsync(Motorcycle motorcycle);
        Task DeleteAsync(Motorcycle motorcycle);
    }
}
