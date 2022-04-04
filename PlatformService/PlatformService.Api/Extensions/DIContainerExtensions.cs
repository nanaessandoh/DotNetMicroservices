namespace PlatformService.Extensions;

public static class DIContainerExtensions
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddScoped<IPlatformDataProvider, PlatformDataProvider>();
        services.AddSingleton<IMessageBusClient, MessageBusClient>();

        return services;
    }

    public static IServiceCollection AddSyncDataService(this IServiceCollection services)
    {
        services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

        return services;
    }
}
