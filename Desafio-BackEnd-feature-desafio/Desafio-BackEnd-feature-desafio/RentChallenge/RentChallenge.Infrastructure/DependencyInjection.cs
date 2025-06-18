using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Messaging;
using RentChallenge.Domain.Interfaces.Repositories;
using RentChallenge.Domain.Interfaces.Storage;
using RentChallenge.Infrastructure.Data;
using RentChallenge.Infrastructure.Menssaging;
using RentChallenge.Infrastructure.Repositories;
using RentChallenge.Infrastructure.Storage;

namespace RentChallenge.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var postgresConnection = configuration.GetConnectionString("Postgres");
            services.AddDbContext<RentDbContext>(options =>
                options.UseNpgsql(postgresConnection,
                x => x.MigrationsAssembly("RentChallenge.Infrastructure")));

            services.AddScoped<IRabbitMqConnectionFactory, RabbitMqConnectionFactory>();

            Console.WriteLine("AddInfrastructure Minio endpoint: " + configuration["Minio:Endpoint"]);

            services.AddSingleton<IFileStorageService>(provider =>
            new MinioFileStorageService(
                configuration["Minio:Endpoint"],
                configuration["Minio:AccessKey"],
                configuration["Minio:SecretKey"]
            ));

            #region Repositories

            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IRentalPlanRepository, RentalPlanRepository>();
            services.AddScoped<IEventMotorcycleRegisteredRepository, EventMotorcycleRegisteredRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

        }
    }
}
