using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace UdemyMicroservice.Shared.Extensions
{
    public static class CommonServiceExtension
    {
        public static IServiceCollection AddCommonServiceExtension(this IServiceCollection services, Type assembly)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));
            services.AddValidatorsFromAssemblyContaining(assembly);
            services.AddMapster();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}