using aninja_tags_service.Models;

namespace aninja_tags_service.SyncDataServices;

public interface IAnimeDataClient
{
    IEnumerable<Anime> ReturnAllAnime();
}