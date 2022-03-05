namespace aninja_tags_service.EventProcessing;

public interface IEventProcessor
{
    Task ProcessEvent(string message);
}