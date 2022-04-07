namespace CommandService.Api.Utilities;

public static class GrpcUtilities
{
    public static async Task MigratePlatformsFromPlatformService(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var services = scope.ServiceProvider;
        var platformDataClinet = services.GetRequiredService<IPlatformDataClient>();
        var logger = services.GetRequiredService<ILogger<Program>>();
        var commandDataProvider = services.GetRequiredService<ICommandDataProvider>();
        await SeedPlatformData(logger, commandDataProvider, platformDataClinet);
    }

    private static async Task SeedPlatformData(ILogger<Program> logger, ICommandDataProvider provider, IPlatformDataClient client)
    {
        logger.LogInformation("---> Seeding platforms form Platform Service.");
        var platforms = await client.ReturnAllPlatforms();

        if (platforms.Any())
        {
            foreach (var platform in platforms)
            {
                if(!(await provider.PlatformExist(platform.ExternalId)))
                {
                    await provider.AddPlatform(platform);
                }
            }
        }
    }
}
