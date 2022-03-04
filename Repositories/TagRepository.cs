using aninja_tags_service.Data;
using aninja_tags_service.Models;
using Microsoft.EntityFrameworkCore;

namespace aninja_tags_service.Repositories;

public class TagRepository : ITagRepository
{
    private AppDbContext _context;

    public TagRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }

    public async Task<IEnumerable<Anime>> GetAllAnimes()
    {
        return await _context.Animes.ToListAsync();
    }

    public async Task CreateAnime(Anime anime)
    {
        await _context.Animes.AddAsync(anime);
    }

    public async Task<bool> AnimeExists(int animeId)
    {
        return await _context.Animes.AnyAsync(x => x.Id == animeId);
    }

    public async Task<bool> ExternalAnimeExists(int externalAnimeId)
    {
        return await _context.Animes.AnyAsync(x => x.ExternalId == externalAnimeId);
    }

    public async Task<IEnumerable<Tag>?> GetTagsForAnime(int animeId)
    {
        var tags = _context.Animes.Where(x => x.Id == animeId).Select(x => x.AnimeTags.Select(y => y.Tag));
        return await tags.FirstOrDefaultAsync();
    }

    public async Task<Tag> GetTag(int animeId, int tagId)
    {
        var tags = await _context.Animes
            .Where(x => x.Id == animeId)
            .Select(x => x.AnimeTags.Select(y => y.Tag))
            .FirstOrDefaultAsync();

        return tags.FirstOrDefault(x => x.Id == tagId);

    }

    public async Task CreateTag(int animeId, Tag tag)
    {

    }
}