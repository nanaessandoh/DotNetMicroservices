namespace PlatformService.Api.AsyncDataServices.Interfaces;

public interface IMessageBusClient
{
    void PublishNewPlatform(PlatformPublishModel PublishedPlatform);
}
