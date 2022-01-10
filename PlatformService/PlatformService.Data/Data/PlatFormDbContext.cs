namespace PlatformService.Data;
public class PlatFormDbContext : DbContext, IPlatformDbContext
{
    public DbSet<Platform> Platforms { get; set;}

    public PlatFormDbContext(DbContextOptions<PlatFormDbContext> options ) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));

        base.OnModelCreating(builder);

        builder.UseIdentityColumns();

        builder.Entity<Platform>(entity => {
            entity.HasData(PlatformSeedData.GetPlatformSeedData());
        });
    }
}