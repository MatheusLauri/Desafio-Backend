using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Domain.Interfaces.Repositories
{
    public interface IRentalPlanRepository
    {
        Task<RentalPlan?> GetByIdAsync(int id);
        Task<IEnumerable<RentalPlan>> GetAllAsync();
    }
}
