using MongoDB.Driver;
using UdemyMicroservice.Discount.Api.Options;
using UdemyMicroservice.Shared.Services;

namespace UdemyMicroservice.Discount.Api.Data.Extensions
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
                MongoOptions? options = sp.GetRequiredService<MongoOptions>();

                return AppDbContext.Create(mongoClient.GetDatabase(options.DatabaseName), sp.GetRequiredService<IIdentityService>());
            });
            return services;
        }
    }
}