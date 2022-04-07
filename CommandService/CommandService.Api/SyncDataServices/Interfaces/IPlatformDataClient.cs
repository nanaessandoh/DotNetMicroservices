namespace CommandService.Api.SyncDataServices.Interfaces;

public interface IPlatformDataClient
{
     Task<IEnumerable<Platform>> ReturnAllPlatforms();
}
