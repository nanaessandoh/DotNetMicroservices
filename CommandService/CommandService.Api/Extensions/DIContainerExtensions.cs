namespace CommandService.Api.Extensions;

public static class DIContainerExtensions
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddScoped<ICommandDataProvider, CommandDataProvider>();

        return services;
    }
}
