using Microsoft.EntityFrameworkCore;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Repositories;
using RentChallenge.Infrastructure.Data;

namespace RentChallenge.Infrastructure.Repositories
{
    public class RentalPlanRepository(RentDbContext context) : IRentalPlanRepository
    {
        private readonly RentDbContext _context = context;

        public async Task<IEnumerable<RentalPlan>> GetAllAsync() => await _context.RentalPlans.ToListAsync();
        public async Task<RentalPlan?> GetByIdAsync(int id) => await _context.RentalPlans.FindAsync(id);
    }
}
