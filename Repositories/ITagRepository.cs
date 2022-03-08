using aninja_tags_service.Models;

namespace aninja_tags_service.Repositories;

public interface ITagRepository
{
    Task<bool> SaveChangesAsync();

    Task<IEnumerable<Anime>> GetAllAnimes();
    Task CreateAnime(Anime anime);
    Task<bool> AnimeExists(int animeId);
    Task<bool> ExternalAnimeExists(int externalAnimeId);
    Task UpdateAnime(Anime anime);

    Task<Tag> GetTag(int tagId);
    Task<IEnumerable<Tag>> GetAllTags();
    Task<Tag> CreateTag(Tag tag);
    
    Task AddAnimeTag(int animeId, Tag tag);
    Task<Tag> GetAnimeTag(int animeId, int tagId);
    Task<IEnumerable<Tag>?> GetTagsForAnime(int animeId);


}