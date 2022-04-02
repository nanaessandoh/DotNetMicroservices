namespace CommandService.Data.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddCommandsDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddEntityFrameworkNpgsql()
            .AddDbContext<ICommandDbContext, CommandDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("CommandsConnection")), ServiceLifetime.Scoped);

        return services;
    }
}
