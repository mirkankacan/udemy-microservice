using MassTransit;
using UdemyMicroservice.Bus.Options;
using UdemyMicroservice.File.Api.Consumer;

namespace UdemyMicroservice.Bus.Extensions
{
    public static class MassTransitConfigurationExtension
    {
        public static IServiceCollection AddMassTransitExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOptions)).Get<BusOptions>()!;
            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<UploadCourseImageCommandConsumer>();

                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });
                    cfg.ReceiveEndpoint("file-microservice.upload-course-image-command.queue", e =>
                    {
                        e.ConfigureConsumer<UploadCourseImageCommandConsumer>(ctx);
                    });
                });
            });

            return services;
        }
    }
}