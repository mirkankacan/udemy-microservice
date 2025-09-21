using Microsoft.EntityFrameworkCore;
using UdemyMicroservice.Order.Domain.Entities;
using UdemyMicroservice.Shared.Services;

namespace UdemyMicroservice.Order.Persistance.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IIdentityService identityService) : DbContext(options)
    {
        public DbSet<Domain.Entities.Order> Orders => Set<Domain.Entities.Order>();
        public DbSet<Domain.Entities.Address> Addresses => Set<Domain.Entities.Address>();
        public DbSet<Domain.Entities.OrderItem> OrderItems => Set<Domain.Entities.OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderPersistanceAssembly).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var baseEntityEntries = ChangeTracker.Entries()
        .Where(e => e.Entity.GetType().BaseType?.IsGenericType == true &&
                   e.Entity.GetType().BaseType.GetGenericTypeDefinition() == typeof(BaseEntity<>));

            var currentUserId = identityService.GetUserId;

            foreach (var entry in baseEntityEntries)
            {
                dynamic entity = entry.Entity;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedAt = DateTime.UtcNow;
                        entity.CreatedBy = currentUserId;
                        break;

                    case EntityState.Modified:
                        entity.UpdatedAt = DateTime.UtcNow;
                        entity.UpdatedBy = currentUserId;
                        entry.Property("CreatedAt").IsModified = false;
                        entry.Property("CreatedBy").IsModified = false;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}