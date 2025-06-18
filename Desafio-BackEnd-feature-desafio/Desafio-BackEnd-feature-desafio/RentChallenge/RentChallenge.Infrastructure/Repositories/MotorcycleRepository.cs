using Microsoft.EntityFrameworkCore;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Repositories;
using RentChallenge.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Infrastructure.Repositories
{
    public class MotorcycleRepository(RentDbContext context) : IMotorcycleRepository
    {
        private readonly RentDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task AddAsync(Motorcycle motorcycle) =>
            await _context.Motorcycles.AddAsync(motorcycle);

        public async Task DeleteAsync(Motorcycle motorcycle)
        {
            if (motorcycle is not null) _context.Motorcycles.Remove(motorcycle);
        }

        public async Task<IEnumerable<Motorcycle>> GetAllAsync() =>
            await _context.Motorcycles.ToListAsync();

        public async Task<Motorcycle?> GetByIdentifierAsync(string identifier) =>
            await _context.Motorcycles.Where(x => x.Identifier == identifier).FirstOrDefaultAsync();

        public Task UpdateAsync(Motorcycle motorcycle)
        {
            _context.Motorcycles.Update(motorcycle);
            return Task.CompletedTask;
        }
    }
}
