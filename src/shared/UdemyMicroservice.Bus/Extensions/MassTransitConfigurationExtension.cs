using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UdemyMicroservice.Bus.Options;

namespace UdemyMicroservice.Bus.Extensions
{
    public static class MassTransitConfigurationExtension
    {
        public static IServiceCollection AddMassTransitCommonExtension(
            this IServiceCollection services,
            IConfiguration configuration,
            Type? assemblyMarkerType = null)
        {
            var busOptions = configuration.GetSection(nameof(BusOptions)).Get<BusOptions>()!;

            services.AddMassTransit(configure =>
            {
                if (assemblyMarkerType != null)
                {
                    configure.AddConsumers(assemblyMarkerType.Assembly);
                }

                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });

                    // EntityName attribute'lı consumer'lar için özel yapılandırma
                    if (assemblyMarkerType != null)
                    {
                        var consumerTypes = assemblyMarkerType.Assembly.GetTypes()
                            .Where(t => t.IsClass && !t.IsAbstract &&
                                   t.GetInterfaces().Any(i => i.IsGenericType &&
                                   i.GetGenericTypeDefinition() == typeof(IConsumer<>)))
                            .ToList();

                        foreach (var consumerType in consumerTypes)
                        {
                            var entityNameAttr = consumerType.GetCustomAttribute<EntityNameAttribute>();

                            if (entityNameAttr != null)
                            {
                                // Reflection ile ConfigureConsumer metodunu çağır
                                var method = typeof(MassTransitConfigurationExtension)
                                    .GetMethod(nameof(ConfigureConsumerEndpoint), BindingFlags.NonPublic | BindingFlags.Static)!
                                    .MakeGenericMethod(consumerType);

                                method.Invoke(null, new object[] { cfg, ctx, entityNameAttr.EntityName });
                            }
                        }
                    }

                    // Diğer consumer'lar için varsayılan yapılandırma
                    cfg.ConfigureEndpoints(ctx);
                });
            });

            return services;
        }

        private static void ConfigureConsumerEndpoint<TConsumer>(
            IRabbitMqBusFactoryConfigurator cfg,
            IBusRegistrationContext ctx,
            string queueName)
            where TConsumer : class, IConsumer
        {
            cfg.ReceiveEndpoint(queueName, e =>
            {
                e.ConfigureConsumer<TConsumer>(ctx);
            });
        }
    }
}