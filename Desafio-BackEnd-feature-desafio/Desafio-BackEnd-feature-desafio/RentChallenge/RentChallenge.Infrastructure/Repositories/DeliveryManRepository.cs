using Microsoft.EntityFrameworkCore;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Repositories;
using RentChallenge.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Infrastructure.Repositories
{
    public class DeliveryManRepository(RentDbContext context) : IDeliveryManRepository
    {
        private readonly RentDbContext _context = context;

        public async Task AddAsync(DeliveryMan deliveryMan) =>
            await _context.DeliveryMen.AddAsync(deliveryMan);

        public async Task<DeliveryMan?> GetByCnhAsync(string cnhNumber) =>
            await _context.DeliveryMen.FirstOrDefaultAsync(e => e.CnhNumber == cnhNumber);

        public async Task<DeliveryMan?> GetByCnpjAsync(string cnpj) =>
            await _context.DeliveryMen.FirstOrDefaultAsync(e => e.Cnpj == cnpj);

        public async Task<DeliveryMan?> GetByIdentifierAsync(string identifier) =>
            await _context.DeliveryMen.Where(e => e.Identifier == identifier).FirstOrDefaultAsync();

        public Task UpdateAsync(DeliveryMan deliveryMan)
        {
            _context.Update(deliveryMan);
            return Task.CompletedTask;
        }
    }
}
