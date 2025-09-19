using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace UdemyMicroservice.Shared.Extensions
{
    public static class VersioningExtension
    {
        public static IServiceCollection AddVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'V";
                opt.SubstituteApiVersionInUrl = true;
            });
            return services;
        }

        public static ApiVersionSet AddVersionSetExtension(this WebApplication app)
        {
            var apiVersionSet = app.NewApiVersionSet()
                 .HasApiVersion(new ApiVersion(1, 0))
                 .HasApiVersion(new ApiVersion(1, 1))
                 .HasApiVersion(new ApiVersion(2, 0))
                 .ReportApiVersions()
                 .Build();
            return apiVersionSet;
        }
    }
}