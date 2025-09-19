using UdemyMicroservice.Discount.Api.Features.Discounts;

namespace UdemyMicroservice.Discount.Api.Data
{
    public static class SeedData
    {
        public static async Task AddSeedDataExtension(this WebApplication app, CancellationToken cancellationToken = default)
        {
            using var scope = app.Services.CreateScope();
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            appDbContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
            if (!await appDbContext.Discounts.AnyAsync(cancellationToken))
            {
                var categories = new List<DiscountEntity>()
                {
                    new DiscountEntity() { Code="QWE123",UserId=NewId.NextGuid(),Rate=25,ExpiredAt=DateTime.UtcNow.AddDays(10) },
                    new DiscountEntity() { Code="ARZ456",UserId=NewId.NextGuid(),Rate=35,ExpiredAt=DateTime.UtcNow.AddDays(5) },
                };
                await appDbContext.Discounts.AddRangeAsync(categories, cancellationToken);
                await appDbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}