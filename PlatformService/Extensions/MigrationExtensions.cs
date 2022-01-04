namespace PlatformService.Extensions;

public static class MigrationExtensions
{
    public static async Task ApplyIPlatformDbContextMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<IPlatformDbContext>();
            //await context.Database.MigrateAsync();
            await DbContextExtensions.SeedPlatformDbContext(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error ocurred during migration.");
        }
    }
}
