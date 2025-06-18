using FluentValidation;
using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Application.Validadors
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        { 
            // O ID do plano de aluguel deve ser maior que zero (valor positivo obrigatório)
            RuleFor(x => x.RentalPlanId).GreaterThan(0);

            // O ID da moto deve ser maior que zero (referência válida a uma moto existente)
            RuleFor(x => x.MotorcycleId).GreaterThan(0);

            // O ID do entregador deve ser maior que zero (referência válida a um entregador existente)
            RuleFor(x => x.DeliveryManId).GreaterThan(0);
        }
    }
}
