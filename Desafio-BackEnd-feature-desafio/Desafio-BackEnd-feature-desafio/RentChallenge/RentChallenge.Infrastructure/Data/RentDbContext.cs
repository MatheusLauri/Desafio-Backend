using Microsoft.EntityFrameworkCore;
using RentChallenge.Domain.Entities;

namespace RentChallenge.Infrastructure.Data
{
    public class RentDbContext : DbContext
    {
        public RentDbContext(DbContextOptions<RentDbContext> options) : base(options) { }

        public DbSet<DeliveryMan> DeliveryMen => Set<DeliveryMan>();
        public DbSet<EventMotorcycleRegistered> EventMotorcycleRegistereds => Set<EventMotorcycleRegistered>();
        public DbSet<Motorcycle> Motorcycles => Set<Motorcycle>();
        public DbSet<Rental> Rentals => Set<Rental>();
        public DbSet<RentalPlan> RentalPlans => Set<RentalPlan>();

    }
}
