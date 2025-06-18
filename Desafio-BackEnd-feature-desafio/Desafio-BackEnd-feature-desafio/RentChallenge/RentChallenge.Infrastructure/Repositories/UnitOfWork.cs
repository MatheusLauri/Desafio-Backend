using RentChallenge.Domain.Interfaces.Repositories;
using RentChallenge.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Infrastructure.Repositories
{
    public class UnitOfWork(RentDbContext context) : IUnitOfWork
    {
        private readonly RentDbContext _context = context;

        public async Task CommitAsync() =>
            await _context.SaveChangesAsync();
    }
}
