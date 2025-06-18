using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RentChallenge.Application.DTOs.Requests.DeliveryMan;
using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Application.Interfaces.APIs;
using RentChallenge.Application.Interfaces.Handlers;
using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Application.Mappers;
using RentChallenge.Application.Messaging;
using RentChallenge.Application.Services.APIs;
using RentChallenge.Application.Services.Handlers;
using RentChallenge.Application.Services.Registrations;
using RentChallenge.Application.Validadors;
using RentChallenge.Application.Validadors.DeliveryMan;
using RentChallenge.Application.Validadors.Motorcycle;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Messaging;
using RentChallenge.Infrastructure.Menssaging;

namespace RentChallenge.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            #region Services

            services.AddScoped<IRentalRegistrationService, RentalRegistrationService>();
            services.AddScoped<IMotorcycleRegistrationService, MotorcycleRegistrationService>();
            services.AddScoped<IDeliveryManRegistrationService, DeliveryManRegistrationService>();

            services.AddScoped<IMotorcycleHandlerService, MotorcycleHandlerService>();
            services.AddScoped<IDeliveryManHandlerService, DeliveryManHandlerService>();
            services.AddScoped<IRentalHandlerService, RentalHandlerService>();

            services.AddScoped<IMotorcycleApiService, MotorcycleApiService>();
            services.AddScoped<IDeliveryManApiService, DeliveryManApiService>();

            services.AddScoped<IEventMotorcycleRegistrationService, EventMotorcycleRegistrationService>();
            services.AddScoped<IEventMotorcycleHandlerService, EventMotorcycleHandlerService>();

            #endregion

            #region Menssaging

            services.AddSingleton<IRabbitMqConnectionFactory, RabbitMqConnectionFactory>();
            services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();

            #endregion

            #region Mapper

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            #endregion

            #region Validators

            services.AddScoped<IValidator<RegisterMotorcycleRequestDTO>, RegisterMotorcycleValidator>();
            services.AddScoped<IValidator<RegisterDeliveryManDTO>, RegisterDeliveryManValidator>();
            services.AddScoped<IValidator<Rental>, RentalValidator>();

            #endregion
        }
    }
}
