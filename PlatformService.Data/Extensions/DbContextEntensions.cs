namespace PlatformService.Data.Extensions;

public static class DbContextEntensions
{
    public static IServiceCollection AddPlatformDbContext(this IServiceCollection services)
    {
        services.AddDbContext<IPlatformDbContext, PlatFormDbContext>(options =>
            options.UseInMemoryDatabase("PlatformInMem"));

        return services;
    }

}
