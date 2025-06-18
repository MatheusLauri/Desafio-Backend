using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Domain.Interfaces.Repositories
{
    public interface IDeliveryManRepository
    {
        Task<DeliveryMan?> GetByCnpjAsync(string cnpj);
        Task<DeliveryMan?> GetByCnhAsync(string cnhNumber);
        Task<DeliveryMan?> GetByIdentifierAsync(string id);
        Task AddAsync(DeliveryMan deliveryMan);
        Task UpdateAsync(DeliveryMan deliveryMan);
    }
}
