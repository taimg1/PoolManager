using Microsoft.EntityFrameworkCore;
using PoolMS.Core.Entities;

namespace PoolMS.Core
{
    public class PoolContext: DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<PoolSize> PoolSizes => Set<PoolSize>();
        public DbSet<Pool> Pools => Set<Pool>();
        public DbSet<Visit> Visits => Set<Visit>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = "Server=.;Database=PoolDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

    }
}
