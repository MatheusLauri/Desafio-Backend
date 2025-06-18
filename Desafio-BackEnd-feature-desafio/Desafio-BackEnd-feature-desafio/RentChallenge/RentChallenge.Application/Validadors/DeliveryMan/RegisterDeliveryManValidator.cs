using FluentValidation;
using RentChallenge.Application.DTOs.Requests.DeliveryMan;
using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RentChallenge.Application.Validadors.DeliveryMan
{
    // Utiliza a biblioteca FluentValidation para definir regras declarativas.
    public class RegisterDeliveryManValidator : AbstractValidator<RegisterDeliveryManDTO>
    {
        public RegisterDeliveryManValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

            RuleFor(x => x.Cnpj).NotEmpty().Must(IsValidCnpj);

            RuleFor(x => x.CnhNumber).NotEmpty();
        }

        // Função auxiliar que valida se o CNPJ tem exatamente 14 dígitos numéricos.
        private bool IsValidCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            var regex = new Regex(@"^\d{14}$");
            return regex.IsMatch(cnpj);
        }
    }

}
