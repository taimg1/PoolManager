using Microsoft.EntityFrameworkCore;
using PoolMS.Core.Entities;

namespace PoolMS.Core
{
    public class PoolContext: DbContext
    {
        public DbSet<User> User => Set<User>();
        public DbSet<Payment> Payment => Set<Payment>();
        public DbSet<Subscription> Subscription => Set<Subscription>();
        public DbSet<Reservation> Reservation => Set<Reservation>();
        public DbSet<PoolSize> PoolSize => Set<PoolSize>();
        public DbSet<Pool> Pool => Set<Pool>();
        public DbSet<Visit> Visit => Set<Visit>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = "Server=.;Database=PoolDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

    }
}
