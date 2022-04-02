namespace CommandService.Data.Data
{
    public class CommandDbContext : DbContext, ICommandDbContext
    {
        public DbSet<Command> Commands { get; set; }
        public DbSet<Platform> Platforms { get; set; }

        public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            base.OnModelCreating(builder);

            builder.Entity<Platform>(entity => {
                entity.HasMany(p => p.Commands)
                .WithOne(p => p.Platform)
                .HasForeignKey(p => p.PlatformId);
            });

            builder.Entity<Command>(entity => {
                entity.HasOne(c => c.Platform)
                .WithMany(c => c.Commands)
                .HasForeignKey(c => c.PlatformId);
            });
        }
    }
}