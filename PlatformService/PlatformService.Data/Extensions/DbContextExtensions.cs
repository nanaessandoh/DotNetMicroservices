using Microsoft.Extensions.Configuration;

namespace PlatformService.Data.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddPlatformDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddEntityFrameworkNpgsql()
            .AddDbContext<IPlatformDbContext, PlatFormDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("PlatformsConnection")), ServiceLifetime.Scoped);

        return services;
    }
}
