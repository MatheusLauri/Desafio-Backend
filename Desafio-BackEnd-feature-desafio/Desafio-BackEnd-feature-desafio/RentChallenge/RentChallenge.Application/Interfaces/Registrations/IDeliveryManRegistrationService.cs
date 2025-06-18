using RentChallenge.Application.DTOs.Requests.DeliveryMan;
using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Application.Interfaces.Registrations
{
    public interface IDeliveryManRegistrationService
    {
        Task EnqueueRegistrationAsync(DeliveryMan deliveryMan);
    }
}
