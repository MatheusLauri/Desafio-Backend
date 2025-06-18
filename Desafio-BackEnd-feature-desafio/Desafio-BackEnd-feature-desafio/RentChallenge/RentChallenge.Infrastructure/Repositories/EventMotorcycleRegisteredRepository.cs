using Microsoft.EntityFrameworkCore;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Repositories;
using RentChallenge.Infrastructure.Data;

namespace RentChallenge.Infrastructure.Repositories
{
    public class EventMotorcycleRegisteredRepository(RentDbContext context) : IEventMotorcycleRegisteredRepository
    {
        private readonly RentDbContext _context = context;

        public async Task AddAsync(EventMotorcycleRegistered eventItem) => 
            await _context.EventMotorcycleRegistereds.AddAsync(eventItem);

        public async Task<IEnumerable<EventMotorcycleRegistered>> GetAllAsync() => 
            await _context.EventMotorcycleRegistereds.ToListAsync();
    }
}
