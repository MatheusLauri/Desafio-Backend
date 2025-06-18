using Microsoft.EntityFrameworkCore;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Repositories;
using RentChallenge.Infrastructure.Data;

namespace RentChallenge.Infrastructure.Repositories
{
    public class RentalRepository(RentDbContext context) : IRentalRepository
    {
        private readonly RentDbContext _context= context;

        public async Task AddAsync(Rental rental) => 
            await _context.Rentals.AddAsync(rental);

        public async Task<IEnumerable<Rental>> GetAllByDeliveryManAsync(int deliveryManId)
            => await _context.Rentals.Where(x => x.DeliveryManId == deliveryManId).ToListAsync();

        public async Task<Rental?> GetByIdAsync(int id) => 
            await _context.Rentals.FindAsync(id);

        public Task UpdateAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            return Task.CompletedTask;
        }
    }
}
