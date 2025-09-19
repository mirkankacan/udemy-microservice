using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using UdemyMicroservice.Catalog.Api.Features.Courses;

namespace UdemyMicroservice.Catalog.Api.Data.Configurations
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToCollection("courses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasElementName("_id");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasElementName("name").HasMaxLength(100);
            builder.Property(x => x.Description).HasElementName("description").HasMaxLength(1000);
            builder.Property(x => x.UserId).HasElementName("user_id");
            builder.Property(x => x.CategoryId).HasElementName("cateogry_id");
            builder.Property(x => x.ImageUrl).HasElementName("image_url").HasMaxLength(200);
            builder.Property(x => x.Price).HasElementName("price");
            builder.OwnsOne(x => x.Feature, f =>
            {
                f.HasElementName("feature");
                f.Property(x => x.Duration).HasElementName("duration");
                f.Property(x => x.Rating).HasElementName("rating");
                f.Property(x => x.EducatorFullName).HasElementName("educator_full_name").HasMaxLength(100);
            });
            builder.Property(x => x.CreatedBy).HasElementName("created_by");
            builder.Property(x => x.CreatedAt).HasElementName("created_at");
            builder.Property(x => x.UpdatedBy).HasElementName("updated_by");
            builder.Property(x => x.UpdatedAt).HasElementName("updated_at");
            builder.Ignore(x => x.Category);
        }
    }
}