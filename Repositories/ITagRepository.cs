using aninja_tags_service.Models;

namespace aninja_tags_service.Repositories;

public interface ITagRepository
{
    Task<bool> SaveChangesAsync();

    Task<IEnumerable<Anime>> GetAllAnimes();
    Task CreateAnime(Anime anime);
    Task<bool> AnimeExists(int animeId);
    Task<bool> ExternalAnimeExists(int externalAnimeId);

    Task<Tag> GetTag(int animeId, int tagId);
    Task<IEnumerable<Tag>> GetAllTags();
    Task AddTag(Tag tag);
    
    Task AddAnimeTag(int animeId, Tag tag);
    Task<IEnumerable<Tag>?> GetTagsForAnime(int animeId);


}