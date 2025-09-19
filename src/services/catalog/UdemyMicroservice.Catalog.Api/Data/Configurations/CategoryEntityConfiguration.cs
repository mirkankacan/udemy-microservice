using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using UdemyMicroservice.Catalog.Api.Features.Categories;

namespace UdemyMicroservice.Catalog.Api.Data.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToCollection("categories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasElementName("_id");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasElementName("name").HasMaxLength(100);
            builder.Property(x => x.CreatedBy).HasElementName("created_by");
            builder.Property(x => x.CreatedAt).HasElementName("created_at");
            builder.Property(x => x.UpdatedBy).HasElementName("updated_by");
            builder.Property(x => x.UpdatedAt).HasElementName("updated_at");
            builder.Ignore(x => x.Courses);
        }
    }
}