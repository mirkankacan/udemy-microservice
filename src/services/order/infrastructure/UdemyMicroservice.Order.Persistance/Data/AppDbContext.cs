using Microsoft.EntityFrameworkCore;

namespace UdemyMicroservice.Order.Persistance.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Domain.Entities.Order> Orders => Set<Domain.Entities.Order>();
        public DbSet<Domain.Entities.Address> Addresses => Set<Domain.Entities.Address>();
        public DbSet<Domain.Entities.OrderItem> OrderItems => Set<Domain.Entities.OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistanceAssembly).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}