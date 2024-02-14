using Microsoft.EntityFrameworkCore;
using PoolMS.Core.Entities;

namespace PoolMS.Core
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) 
        {

        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<SubType> SubTypes => Set<SubType>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<PoolSize> PoolSizes => Set<PoolSize>();
        public DbSet<Pool> Pools => Set<Pool>();
        public DbSet<Visit> Visits => Set<Visit>();
        public DbSet<Role> Roles => Set<Role>();

    }
}
