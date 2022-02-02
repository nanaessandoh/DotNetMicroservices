namespace PlatformService.Api.SyncDataServices.Interfaces;

public interface ICommandDataClient
{
    Task SendPlatformToCommand(PlatformViewModel platform);
}
