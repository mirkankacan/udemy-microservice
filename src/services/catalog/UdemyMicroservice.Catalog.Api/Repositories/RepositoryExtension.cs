using MongoDB.Driver;
using UdemyMicroservice.Catalog.Api.Options;

namespace UdemyMicroservice.Catalog.Api.Repositories
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositoryExtensions(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var options = sp.GetRequiredService<MongoOptions>();
                return new MongoClient(options.ConnectionString);
            });
            services.AddScoped(sp =>
            {
                var mongoClient = sp.GetRequiredService<IMongoClient>();
                var options = sp.GetRequiredService<MongoOptions>();
                return AppDbContext.Create(mongoClient.GetDatabase(options.DatabaseName));
            });
            return services;
        }
    }
}