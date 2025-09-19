using MongoDB.Driver;
using UdemyMicroservice.Catalog.Api.Options;

namespace UdemyMicroservice.Catalog.Api.Data.Extensions
{
    public static class DataExtension
    {
        public static IServiceCollection AddDataExtensions(this IServiceCollection services)
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