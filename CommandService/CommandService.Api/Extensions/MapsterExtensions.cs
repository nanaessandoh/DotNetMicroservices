using CommandService.Data;

namespace CommandService.Api.Extensions;

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
        config.NewConfig<Command, CommandViewModel>();
        config.NewConfig<CommandCreateModel, Command>();
        config.NewConfig<PlatformPublishModel, Platform>()
            .Ignore(dest => dest.Id)
            .Map(dest => dest.ExternalId, src => src.Id);
        config.NewConfig<GrpcPlatformModel, Platform>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Commands)
            .Map(dest => dest.ExternalId, src => src.PlaformId)
            .Map(dest => dest.Name, src => src.Name);

        config.Compile();

        return config;
    }

}
