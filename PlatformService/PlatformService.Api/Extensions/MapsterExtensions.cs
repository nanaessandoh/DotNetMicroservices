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
        config.NewConfig<Platform, GrpcPlatformModel>()
            .Map(dest => dest.PlaformId, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Publisher, src => src.Publisher);
        config.Compile();

        return config;
    }
}
