using CommandService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CommandService.Data.Interfaces
{
    public interface ICommandDbContext
    {
        DbSet<Command> Commands { get; set; }
        DbSet<Platform> Platforms { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DatabaseFacade Database { get; }
    }
}