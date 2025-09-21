using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UdemyMicroservice.Order.Persistance.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedNever();
            builder.Property(o => o.Code)
                   .IsRequired()
                   .HasMaxLength(maxLength: 10);
            builder.Property(o => o.BuyerId)
                   .IsRequired();
            builder.Property(o => o.Status)
                   .IsRequired();
            builder.Property(o => o.LastPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(o => o.PaymentId);
            builder.Property(o => o.DiscountRate).HasColumnType("float");

            builder.HasMany(o => o.OrderItems)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.Address)
                   .WithMany(a => a.Orders)
                   .HasForeignKey(o => o.AddressId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}