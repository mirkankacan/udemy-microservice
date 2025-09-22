using Microsoft.EntityFrameworkCore;
using UdemyMicroservice.Shared.Services;

namespace UdemyMicroservice.Payment.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IIdentityService identityService) : DbContext(options)
    {
        public DbSet<Features.Payments.Payment> Payments => Set<Features.Payments.Payment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentAssembly).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}