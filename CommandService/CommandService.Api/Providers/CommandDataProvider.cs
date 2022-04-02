using CommandService.Api.Exceptions;

namespace CommandService.Api.Providers;

public class CommandDataProvider : ICommandDataProvider
{
    private readonly ICommandDbContext _context;

    public CommandDataProvider(ICommandDbContext context)
    {
        _context = context;
    }

    public async Task<Command> AddCommand(int platformId, Command command)
    {
        _ = command ?? throw new ArgumentNullException(nameof(command));

        if (!await PlatformExist(platformId))
        {
            throw new BadRequestException("A command cannot be created for a platform that does not exist.");
        }

        bool commandExist = (await GetCommandsForPlatform(platformId))
            .Any(c => c.HowTo == command.HowTo && c.CommandLine == command.CommandLine);

        if (commandExist)
        {
            var platform = await GetPlatform(platformId);
            throw new BadRequestException($"A command already exist for platform: {platform.Name}.");
        }

        command.PlatformId = platformId;
        await _context.Commands.AddAsync(command);
        await _context.SaveChangesAsync();

        return command;
    }

    public async Task<Platform> AddPlatform(Platform platform)
    {
        _ = platform ?? throw new ArgumentNullException(nameof(platform));
        await _context.Platforms.AddAsync(platform);
        await _context.SaveChangesAsync();

        return platform;
    }

    public async Task DeleteCommand(int platformId, int commandId)
    {
        var command = await GetCommand(platformId, commandId);
        _ = command ?? throw new BadRequestException($"Command does not exist with Id = {commandId}.");

        _context.Commands.Remove(command);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Platform>> GetAllPlatforms()
    {
        return await _context.Platforms
            .Include(x => x.Commands)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Command> GetCommand(int platformId, int commandId)
    {
        var command = (await GetAllCommands())
            .FirstOrDefault(c => c.Id == commandId && c.PlatformId == platformId);

        _ = command ?? throw new NotFoundException("Command does not exist");

        return command;
    }


    public async Task<IEnumerable<Command>> GetCommandsForPlatform(int plaformId)
    {
        return (await GetAllCommands())
            .Where(c => c.PlatformId == plaformId);
    }

    public async Task<Platform> GetPlatform(int platformId)
    {
        var platform =  (await GetAllPlatforms())
                .FirstOrDefault(p => p.Id == platformId);

        _ = platform ?? throw new NotFoundException("Platform does not exist");

        return platform;
    }

    public async Task<bool> PlatformExist(int platformId)
    {
        return (await GetAllPlatforms())
            .Any(p => p.Id == platformId);
    }

    private async Task<IEnumerable<Command>> GetAllCommands()
    {
        return await _context.Commands
        .Include(x => x.Platform)
        .ToListAsync();
    }
}
