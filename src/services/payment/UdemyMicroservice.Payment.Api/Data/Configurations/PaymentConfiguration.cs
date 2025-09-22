using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UdemyMicroservice.Payment.Api.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Features.Payments.Payment>
    {
        public void Configure(EntityTypeBuilder<Features.Payments.Payment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.OrderCode).IsRequired().HasMaxLength(10);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.Amount).IsRequired().HasPrecision(18, 2);
            builder.Property(p => p.Status).IsRequired();
        }
    }
}