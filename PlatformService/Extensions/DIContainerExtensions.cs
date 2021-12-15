namespace PlatformService.Extensions;

public static class DIContainerExtensions
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddScoped<IPlatformDataProvider, PlatformDataProvider>();

        return services;
    }
}
