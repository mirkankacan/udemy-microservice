using Microsoft.EntityFrameworkCore.ChangeTracking;
using MongoDB.Driver;
using System.Reflection;
using UdemyMicroservice.Shared.Services;

namespace UdemyMicroservice.Discount.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IIdentityService identityService) : DbContext(options)
    {
        public DbSet<Features.Discounts.Discount> Discounts { get; set; }

        public static AppDbContext Create(IMongoDatabase database, IIdentityService identityService)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);
            return new AppDbContext(optionsBuilder.Options, identityService);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Collection = Table, Document = Row, Field = Column

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            var currentUserId = identityService.GetUserId;
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = currentUserId;
                        entry.Property(e => e.UpdatedBy).IsModified = false;
                        entry.Property(e => e.UpdatedAt).IsModified = false;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = currentUserId;
                        entry.Property(e => e.CreatedBy).IsModified = false;
                        entry.Property(e => e.CreatedAt).IsModified = false;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}