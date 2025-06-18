using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Domain.Interfaces.Repositories
{
    public interface IRentalRepository
    {
        Task AddAsync(Rental rental);
        Task<Rental?> GetByIdAsync(int id);
        Task UpdateAsync(Rental rental);
        Task<IEnumerable<Rental>> GetAllByDeliveryManAsync(int deliveryManId);
    }
}
