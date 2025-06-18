using RentChallenge.Application.DTOs.Requests.DeliveryMan;
using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Application.Interfaces.APIs
{
    public interface IDeliveryManApiService
    {
        Task RegisterAsync(RegisterDeliveryManDTO deliveryMan);
        Task UploadCnhImage(Stream stream, string deliveryManIdentifier, string contentType);
    }
}
