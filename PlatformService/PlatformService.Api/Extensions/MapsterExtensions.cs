namespace PlatformService.Extensions;

public static class MapsterProfiles
{
    public static TypeAdapterConfig GetMapsterConfig()
    {
        var config = new TypeAdapterConfig();

        // Source --> Target
        config.NewConfig<Platform, PlatformViewModel>();
        config.NewConfig<PlatformCreateModel, Platform>();

        config.Compile();

        return config;
    }

    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        services.AddSingleton(MapsterProfiles.GetMapsterConfig());
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
