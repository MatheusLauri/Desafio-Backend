using Microsoft.Extensions.DependencyInjection;
using RentChallenge.Application.Services.Handlers;
using RentChallenge.Application.Services.Registrations;
using RentChallenge.Consumer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Consumer
{
    public static class DependencyInjection
    {
        public static void AddConsumer(this IServiceCollection services)
        {
            services.AddSingleton<RabbitMqListener>();
            services.AddHostedService<RabbitMqBackgroundService>();

        }
    }
}
