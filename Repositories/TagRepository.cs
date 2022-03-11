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
        return await _context.Animes.Include(x => x.AnimeTags).ToListAsync();
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

    public async Task<Anime?> UpdateAnime(Anime anime)
    {
        var dbItem = await _context.Animes.FirstOrDefaultAsync(x => x.ExternalId == anime.ExternalId);
        if(dbItem is null) return null;
        if(dbItem.TranslatedTitle == anime.TranslatedTitle) return null;
        dbItem.TranslatedTitle = anime.TranslatedTitle;
        return _context.Animes.Update(dbItem).Entity;
    }

    public async Task<Anime?> GetAnime(int animeId)
    {
        return await _context.Animes.Include(x => x.AnimeTags).FirstOrDefaultAsync(x => x.ExternalId == animeId);
    }

    public async Task<Tag?> GetTag(int tagId)
    {
        return await _context.Tags.FirstOrDefaultAsync(x => x.Id == tagId);
    }
    
    public async Task<IEnumerable<Tag>> GetAllTags()
    {
        var tags = await _context.Tags.ToListAsync();
        return tags;
    }

    public async Task<Tag> CreateTag(Tag tag)
    {
        var addedTag = await _context.Tags.AddAsync(tag);
        return addedTag.Entity;
    }

    public async Task<Tag> UpdateTag(Tag tag)
    {
        var updatedTag = _context.Tags.Update(tag);
        return await Task.FromResult(updatedTag.Entity);
    }

}