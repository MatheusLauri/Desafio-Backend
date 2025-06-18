using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Domain.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public int DeliveryManId { get; set; }
        public int MotorcycleId { get; set; }
        public int RentalPlanId { get; set; }
        public decimal DailyCost { get; set; }
        public DateTime InitalDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime? EndDate { get; set; }

        public RentalPlan? RentalPlan { get; set; }
    }
}
