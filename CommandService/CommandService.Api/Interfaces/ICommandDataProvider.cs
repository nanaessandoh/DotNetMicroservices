namespace CommandService.Api.Interfaces
{
    public interface ICommandDataProvider
    {
        // Platforms
        Task<IEnumerable<Platform>> GetAllPlatforms();
        Task<Platform> GetPlatform(int platformId);
        Task<Platform> AddPlatform(Platform platform);
        Task<bool> PlatformExist(int platformId);

        // Commands
        Task<IEnumerable<Command>> GetCommandsForPlatform(int plaformId);
        Task<Command> GetCommand(int platformId, int commandId);
        Task<Command> AddCommand(int plaformId, Command command);
        Task DeleteCommand(int platformId, int commandId);
    }
}