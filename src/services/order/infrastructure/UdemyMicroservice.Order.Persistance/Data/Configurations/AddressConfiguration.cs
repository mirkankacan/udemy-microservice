using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UdemyMicroservice.Order.Domain.Entities;

namespace UdemyMicroservice.Order.Persistance.Data.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(a => a.Street).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Province).IsRequired().HasMaxLength(100);

            builder.Property(a => a.District).IsRequired().HasMaxLength(100);
            builder.Property(a => a.ZipCode).IsRequired().HasMaxLength(20);
            builder.Property(a => a.Line).IsRequired().HasMaxLength(300);

            builder.HasMany(a => a.Orders)
                   .WithOne(o => o.Address)
                   .HasForeignKey(o => o.AddressId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}