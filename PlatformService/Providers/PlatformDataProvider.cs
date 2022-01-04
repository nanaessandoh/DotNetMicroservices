namespace PlatformService.Providers;

public class PlatformDataProvider : IPlatformDataProvider
{
    private readonly IPlatformDbContext _context;

    public PlatformDataProvider(IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Platform>> GetAll()
    {
        return await _context.Platforms.ToListAsync();
    }

    public async Task Add(Platform platform)
    {
        _ = platform ?? throw new ArgumentNullException(nameof(platform));
        await _context.Platforms.AddAsync(platform);
        await _context.SaveChangesAsync();
    }

    public async Task<Platform> Get(int id)
    {
        return (await GetAll())
            .FirstOrDefault(x => x.Id == id);
    }

    public async Task Delete(int id)
    {
        var platform = await Get(id);

        if (platform is null)
        {
            // throw exception
        }

        _context.Platforms.Remove(platform);
        await _context.SaveChangesAsync();
    }
}
