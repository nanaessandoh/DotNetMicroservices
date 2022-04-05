using System.Text.Json;

namespace CommandService.Api.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory  = scopeFactory;
        _mapper = mapper;
    }

    public async Task ProcessEvent(string message)
    {
        var eventType = GetEventType(message);

        if (eventType == EventType.PlatformPublished)
        {
            await AddPlatform(message);
        }
    }

    private async Task AddPlatform(string message)
    {
        using var scope = _scopeFactory.CreateScope();
        var commandDataProvider = scope.ServiceProvider.GetRequiredService<ICommandDataProvider>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<EventProcessor>>();

        var platformPublishModel = JsonSerializer.Deserialize<PlatformPublishModel>(message);
        try
        {
            var plaform = _mapper.Map<Platform>(platformPublishModel);
            await commandDataProvider.AddPlatform(plaform);
            logger.LogInformation("Plaform received and added to DB");
        }
        catch (Exception ex)
        {
            logger.LogError("Could not add Plaform to DB", ex);
        }
    }

    private EventType GetEventType(string notificationMessage)
    {
        var eventType = JsonSerializer.Deserialize<GenericEventDTO>(notificationMessage);

        return eventType.Event switch
        {
            "Platform_Published" => EventType.PlatformPublished,
            _ => EventType.Undetermined
        };
    }
}
