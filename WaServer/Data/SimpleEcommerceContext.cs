using Microsoft.EntityFrameworkCore;
using WaServer.Data.Entities;

namespace WaServer.Data
{
    public class SimpleEcommerceContext : DbContext
    {
        public SimpleEcommerceContext(DbContextOptions<SimpleEcommerceContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(true);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryTeam> DeliveryTeams { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
