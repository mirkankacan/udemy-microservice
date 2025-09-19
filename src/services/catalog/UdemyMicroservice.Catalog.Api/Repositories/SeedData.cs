using UdemyMicroservice.Catalog.Api.Features.Categories;
using UdemyMicroservice.Catalog.Api.Features.Courses;

namespace UdemyMicroservice.Catalog.Api.Repositories
{
    public static class SeedData
    {
        public static async Task AddSeedDataExtension(this WebApplication app, CancellationToken cancellationToken = default)
        {
            using var scope = app.Services.CreateScope();
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            appDbContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
            if (!await appDbContext.Categories.AnyAsync(cancellationToken))
            {
                var categories = new List<Category>()
                {
                    new Category() { Name = "Asp.Net Core" },
                    new Category() { Name = "Entity Framework Core"},
                    new Category() { Name = "Design Patterns" },
                    new Category() { Name = "Microservices"},
                    new Category() { Name = "Docker" },
                    new Category() { Name = "Kubernetes" }
                };
                await appDbContext.Categories.AddRangeAsync(categories, cancellationToken);
                await appDbContext.SaveChangesAsync(cancellationToken);
            }
            if (!await appDbContext.Courses.AnyAsync(cancellationToken))
            {
                var categoryId = await appDbContext.Categories.Select(x => x.Id).FirstAsync();
                var courses = new List<Course>()
                {
                    new Course() { Name = "Asp.Net Core Fundamentals",Description="Learn the fundamentals of Asp.Net Core",Price=20,UserId=NewId.NextGuid(),Feature=new Feature{Duration=10,Rating=5,EducatorFullName="John Bell"}, CategoryId = categoryId },
                    new Course() { Name = "Entity Framework Core Basics", Description="Get started with Entity Framework Core", Price=25,UserId=NewId.NextGuid(),Feature=new Feature{Duration=12,Rating=4,EducatorFullName="Jane Smith"}, CategoryId = categoryId },
                    new Course() { Name = "Design Patterns in C#", Description="Explore common design patterns in C#", Price=30,UserId=NewId.NextGuid(),Feature=new Feature{Duration=8,Rating=5,EducatorFullName="Mike Johnson"}, CategoryId = categoryId },
                    new Course() { Name = "Microservices Architecture", Description="Understand microservices architecture", Price=35,UserId=NewId.NextGuid(),Feature=new Feature{Duration=15,Rating=4,EducatorFullName="Emily Davis"}, CategoryId = categoryId },
                    new Course() { Name = "Docker for Beginners", Description="Introduction to Docker", Price=40,UserId=NewId.NextGuid(),Feature=new Feature{Duration=10,Rating=5,EducatorFullName="Chris Lee"}, CategoryId = categoryId },
                    new Course() { Name = "Kubernetes Deep Dive", Description="In-depth look at Kubernetes", Price=45,UserId=NewId.NextGuid(),Feature=new Feature{Duration=20,Rating=4,EducatorFullName="Sarah Wilson"}, CategoryId = categoryId }
                };
                await appDbContext.Courses.AddRangeAsync(courses, cancellationToken);
                await appDbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}