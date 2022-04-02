namespace CommandService.Api.Extensions
{
    public static class MigrationExtensions
    {
        public static async Task ApplyCommandDbContextMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ICommandDbContext>();
            var logger = services.GetRequiredService<ILogger<Program>>();

            try
            {
                logger.LogInformation("Attempting to run command service migrations.");
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error ocurred during migration.");
            }
        }
    }
}