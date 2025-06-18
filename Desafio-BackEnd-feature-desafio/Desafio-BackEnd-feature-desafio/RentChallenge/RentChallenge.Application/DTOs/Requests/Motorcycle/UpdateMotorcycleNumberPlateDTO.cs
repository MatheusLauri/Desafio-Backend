using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentChallenge.Application.DTOs.Requests.Motorcycle
{
    public class UpdateMotorcycleNumberPlateDTO
    {
        [JsonPropertyName("placa")]
        public string NumberPlate { get; set; }
    }
}
