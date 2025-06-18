using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Application.Interfaces.Handlers
{
    public interface IDeliveryManHandlerService
    {
        Task HandleAsync(DeliveryMan deliveryMan);
    }
}
