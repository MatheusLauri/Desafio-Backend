using FluentValidation;
using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RentChallenge.Application.Validadors.Motorcycle
{
    // Utiliza FluentValidation para tornar a definição das regras clara e fluente.
    public class RegisterMotorcycleValidator : AbstractValidator<RegisterMotorcycleRequestDTO>
    {
        public RegisterMotorcycleValidator()
        {
            // Identificador é obrigatório (ex: chassi, código interno, etc.)
            RuleFor(x => x.Identifier).NotEmpty();

            // Ano da moto deve estar entre 2000 e o ano atual
            RuleFor(x => x.Year).InclusiveBetween(2000, DateTime.UtcNow.Year);

            // Modelo da moto não pode ser vazio
            RuleFor(x => x.Model).NotEmpty();

            // Placa não pode ser vazia e deve conter exatamente 7 caracteres alfanuméricos (sem traços ou espaços)
            RuleFor(x => x.NumberPlate).NotEmpty()
                                       .Must((motorcycle) =>
                                       {
                                           var clean = Regex.Replace(motorcycle ?? string.Empty, "[^a-zA-Z0-9]", "");
                                           return clean.Length == 7;
                                       });
        }
    }
}
