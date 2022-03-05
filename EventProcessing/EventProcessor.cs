using System.Text.Json;
using aninja_tags_service.Dtos;
using aninja_tags_service.Models;
using aninja_tags_service.Repositories;
using AutoMapper;

namespace aninja_tags_service.EventProcessing;

enum EventType
{
    AnimePublished,
    Undetermined
}

public class EventProcessor : IEventProcessor
{
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public EventProcessor(IMapper mapper, IServiceScopeFactory scopeFactory)
    {
        _mapper = mapper;
        _scopeFactory = scopeFactory;
    }

    public async Task ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);
        switch (eventType)
        {
            case EventType.AnimePublished:
                await AddAnime(message);
                break;
            default:
                break;
        }
    }

    private async Task AddAnime(string message)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<ITagRepository>();
            var animePublishedDto = JsonSerializer.Deserialize<AnimePublishedDto>(message);
            var anime = _mapper.Map<Anime>(animePublishedDto);
            if (!await repo.ExternalAnimeExists(anime.ExternalId))
            {
                await repo.CreateAnime(anime);
                await repo.SaveChangesAsync();
            }
        }
    }

    private EventType DetermineEvent(string message)
    {
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);
        return eventType?.Event switch
        {
            "Anime_Published" => EventType.AnimePublished,
            _ => EventType.Undetermined
        };
    }
}