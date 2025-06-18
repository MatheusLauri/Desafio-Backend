using RentChallenge.Application.Data.DTOs.Requests;
using RentChallenge.Application.DTOs.Requests;
using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Application.Data.Mappers
{
    public static class MotorcycleMapper
    {
        public static Motorcycle ToEntity(MotorcycleRequestDTO dto)
        {
            return new Motorcycle
            {
                NumeberPlate = dto.NumberPlate,
                Identifier = dto.Identifier,
                Year = dto.Year,
                Model = dto.Model
            };
        }
    }
}
