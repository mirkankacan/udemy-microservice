using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UdemyMicroservice.Bus.Options;

namespace UdemyMicroservice.Bus.Extensions
{
    public static class MassTransitConfigurationExtension
    {
        public static IServiceCollection AddMassTransitCommonExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOptions)).Get<BusOptions>()!;
            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });
                    //cfg.ConfigureEndpoints(ctx);
                    //cfg.ReceiveEndpoint("create-order-event.queue", e =>
                    //{
                    //    e.ConfigureConsumer<CreateOrderEventConsumer>(ctx);
                    //});
                });
            });

            return services;
        }
    }
}