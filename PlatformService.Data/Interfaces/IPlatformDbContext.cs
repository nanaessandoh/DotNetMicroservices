namespace PlatformService.Data.Interfaces;

public interface IPlatformDbContext
{
    DbSet<Platform> Platforms { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    DatabaseFacade Database { get; }
}
