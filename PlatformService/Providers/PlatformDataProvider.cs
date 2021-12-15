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

    public async Task CreatePlatform(Platform platform)
    {
        _ = platform ?? throw new ArgumentNullException(nameof(platform));
        await _context.Platforms.AddAsync(platform);
    }

    public async Task<Platform> Get(int id)
    {
        return (await GetAll())
            .FirstOrDefault(x => x.Id == id);
    }
}
