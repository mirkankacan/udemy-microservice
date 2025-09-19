using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using UdemyMicroservice.Catalog.Api.Features.Courses;

namespace UdemyMicroservice.Catalog.Api.Repositories
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToCollection("courses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasElementName("name").HasMaxLength(100);
            builder.Property(x => x.Description).HasElementName("description").HasMaxLength(1000);
            builder.Property(x => x.UserId).HasElementName("userId");
            builder.Property(x => x.CategoryId).HasElementName("categoryId");
            builder.Property(x => x.ImageUrl).HasElementName("imageUrl").HasMaxLength(200);
            builder.Property(x => x.Price).HasElementName("price");
            builder.OwnsOne(x => x.Feature, f =>
            {
                f.HasElementName("feature");
                f.Property(x => x.Duration).HasElementName("duration");
                f.Property(x => x.Rating).HasElementName("rating");
                f.Property(x => x.EducatorFullName).HasElementName("educatorFullName").HasMaxLength(100);
            });
            builder.Ignore(x => x.Category);
        }
    }
}