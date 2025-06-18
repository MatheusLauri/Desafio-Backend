using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Domain.Entities
{
    public class RentalPlan
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int DurationInDays { get; set; }
        public decimal DailyValue { get; set; }
        public decimal? FinePercentage { get; set; } 
        public decimal? AditionalDailyValue { get; set; } 
    }
}
