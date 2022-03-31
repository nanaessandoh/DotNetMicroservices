namespace PlatformService.Extensions;

public static class MigrationExtensions
{
    public static async Task ApplyIPlatformDbContextMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<IPlatformDbContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            logger.LogInformation("Attempting to run migrations.");
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error ocurred during migration.");
        }

    }
}
