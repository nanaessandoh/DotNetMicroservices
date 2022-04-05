namespace PlatformService.Extensions;

public static class MapsterExtensions
{

    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        services.AddSingleton(GetMapsterConfig());
        services.AddSingleton<IMapper, ServiceMapper>();

        return services;
    }

    private static TypeAdapterConfig GetMapsterConfig()
    {
        var config = new TypeAdapterConfig();

        // Source --> Target
        config.NewConfig<Platform, PlatformViewModel>();
        config.NewConfig<PlatformCreateModel, Platform>();
        config.NewConfig<PlatformViewModel, PlatformPublishModel>()
            .Map(dest => dest.Event, src => "Platform_Published");

        config.Compile();

        return config;
    }
}
