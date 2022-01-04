namespace PlatformService.Interfaces;

public interface IPlatformDataProvider
{
    Task<IEnumerable<Platform>> GetAll();
    Task<Platform> Get(int id);
    Task Add(Platform platform);
    Task Delete(int id);

}
