using Microsoft.Extensions.Options;

namespace UdemyMicroservice.Catalog.Api.Options
{
    public static class OptionsExtension
    {
        public static IServiceCollection AddOptionsExtensions(this IServiceCollection services)
        {
            services.AddOptions<MongoOptions>()
                .BindConfiguration(nameof(MongoOptions))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoOptions>>().Value);
            return services;
        }
    }
}