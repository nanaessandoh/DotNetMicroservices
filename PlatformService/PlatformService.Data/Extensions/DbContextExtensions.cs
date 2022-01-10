namespace PlatformService.Data.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddPlatformDbContext(this IServiceCollection services)
    {
        services.AddDbContext<IPlatformDbContext, PlatFormDbContext>(options =>
            options.UseInMemoryDatabase("PlatformInMem"));

        return services;
    }

    public static async Task SeedPlatformDbContext(this IPlatformDbContext context)
    {
        if (context.Platforms.Any())
        {
            return;
        }

        var platforms = PlatformSeedData.GetPlatformSeedData();
        await context.Platforms.AddRangeAsync(platforms);
        await context.SaveChangesAsync();
    }

}
