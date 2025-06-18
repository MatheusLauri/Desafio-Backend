using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using RentChallenge.Domain.Entities;

namespace RentChallenge.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task ApplySeedWithRetryAsync(IServiceProvider services)
        {
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(5));

            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<RentDbContext>();

            await retryPolicy.ExecuteAsync(async () =>
            {
                await db.Database.MigrateAsync();

                if (!db.RentalPlans.Any())
                {
                    db.RentalPlans.AddRange(
                        new RentalPlan { Name = "7 dias", DurationInDays = 7, DailyValue = 30, FinePercentage = 0.20m },
                        new RentalPlan { Name = "15 dias", DurationInDays = 15, DailyValue = 28, FinePercentage = 0.40m },
                        new RentalPlan { Name = "30 dias", DurationInDays = 30, DailyValue = 22 },
                        new RentalPlan { Name = "45 dias", DurationInDays = 45, DailyValue = 20 },
                        new RentalPlan { Name = "50 dias", DurationInDays = 50, DailyValue = 18 }
                    );
                    await db.SaveChangesAsync();
                }
            });
        }
    }
}
