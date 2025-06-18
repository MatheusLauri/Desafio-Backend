using RentChallenge.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Domain.Entities
{
    public class DeliveryMan
    {
        public int Id { get; set; }
        public string Identifier { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Cnpj { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string CnhNumber { get; set; } = null!;
        public CnhType CnhType { get; set; }
        public string? CnhImageUrl { get; set; }
    }
}
