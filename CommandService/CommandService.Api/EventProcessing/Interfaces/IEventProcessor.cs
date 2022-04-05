namespace CommandService.Api.EventProcessing.Interfaces;

public interface IEventProcessor
{
    Task ProcessEvent(string message);
}
