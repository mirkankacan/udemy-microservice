using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using UdemyMicroservice.Discount.Api.Features.Discounts;

namespace UdemyMicroservice.Discount.Api.Data.Configurations
{
    public class DiscountEntityConfiguration : IEntityTypeConfiguration<Features.Discounts.Discount>
    {
        public void Configure(EntityTypeBuilder<Features.Discounts.Discount> builder)
        {
            builder.ToCollection("discounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasElementName("_id");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.UserId).HasElementName("user_id");
            builder.Property(x => x.Rate).HasElementName("rate");
            builder.Property(x => x.Code).HasElementName("code").HasMaxLength(10);
            builder.Property(x => x.ExpiredAt).HasElementName("expired_at");
            builder.Property(x => x.CreatedBy).HasElementName("created_by");
            builder.Property(x => x.CreatedAt).HasElementName("created_at");
            builder.Property(x => x.UpdatedBy).HasElementName("updated_by");
            builder.Property(x => x.UpdatedAt).HasElementName("updated_at");
        }
    }
}