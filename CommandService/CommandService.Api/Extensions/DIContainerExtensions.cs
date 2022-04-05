namespace CommandService.Api.Extensions;

public static class DIContainerExtensions
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddScoped<ICommandDataProvider, CommandDataProvider>();
        services.AddSingleton<IEventProcessor, EventProcessor>();

        return services;
    }

    public static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<MessageBusSubscriber>();

        return services;
    }
}
