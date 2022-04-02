using CommandService.Api.DTOs;

namespace CommandService.Api.Extensions;

public static class MapsterExtensions
{
    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        services.AddSingleton(GetMapsterConfig());
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }

    private static TypeAdapterConfig GetMapsterConfig()
    {
        var config = new TypeAdapterConfig();

        // Source --> Target
        config.NewConfig<Platform, PlatformViewModel>();
        config.NewConfig<Command, CommandViewModel>();
        config.NewConfig<CommandCreateModel, Command>();

        config.Compile();

        return config;
    }

}
