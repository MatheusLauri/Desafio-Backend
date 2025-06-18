using FluentValidation;
using RentChallenge.Application.DTOs.Requests.Motorcycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RentChallenge.Application.Validadors.Motorcycle
{
    public class UpdateMotorcycleNumberPlateValidator : AbstractValidator<UpdateMotorcycleNumberPlateDTO>
    {
        public UpdateMotorcycleNumberPlateValidator()
        {
            RuleFor(x => x.NumberPlate).NotEmpty()
                                       .Must((motorcycle) =>
                                       {
                                           var clean = Regex.Replace(motorcycle ?? string.Empty, "[^a-zA-Z0-9]", "");
                                           return clean.Length == 7;
                                       });
        }
    }
}
