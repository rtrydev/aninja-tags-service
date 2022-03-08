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

    public async Task UpdateAnime(Anime anime)
    {
        var dbItem = await _context.Animes.FirstOrDefaultAsync(x => x.ExternalId == anime.ExternalId);
        if(dbItem is null) return;
        if(dbItem.TranslatedTitle == anime.TranslatedTitle) return;
        dbItem.TranslatedTitle = anime.TranslatedTitle;
        _context.Animes.Update(dbItem);
    }

    public async Task<Tag> GetTag(int tagId)
    {
        return await _context.Tags.FirstOrDefaultAsync(x => x.Id == tagId);
    }

    public async Task<IEnumerable<Tag>?> GetTagsForAnime(int animeId)
    {
        var animeTags = await _context.Animes
            .Include(x => x.AnimeTags)
            .Where(x => x.ExternalId == animeId)
            .SelectMany(x => x.AnimeTags.Select(y => y.TagId))
            .ToListAsync();
            
        if (animeTags is null) return null;
        var tags = await _context.Tags.Where(x => animeTags.Contains(x.Id)).ToListAsync();
        
        return tags;
    }

    public async Task<IEnumerable<Tag>?> RemoveAnimeTag(int animeId, Tag tag)
    {
        var anime = await _context.Animes.Include(x => x.AnimeTags).FirstOrDefaultAsync(x => x.ExternalId == animeId);
        if (anime is null) return null;
        if(anime.AnimeTags is null) return null;
        var animeTag = anime.AnimeTags.FirstOrDefault(x => x.TagId == tag.Id);
        if(animeTag is null) return null;
        anime.AnimeTags.Remove(animeTag);
        var result = _context.Animes.Update(anime);
        var tags = anime.AnimeTags.Select(x => _context.Tags.FirstOrDefault(y => y.Id == x.TagId)).ToList();
        return tags;
    }

    public async Task<Tag> GetAnimeTag(int animeId, int tagId)
    {
        var animes = await _context.Animes
            .FirstOrDefaultAsync(x => x.Id == animeId);
        var tags = animes.AnimeTags.Select(x => x.Tag);

        return tags.FirstOrDefault(x => x.Id == tagId);

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

    public async Task AddAnimeTag(int animeId, Tag tag)
    {
        var tagEntity = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tag.Id);
        if(tagEntity is null) return;
        var anime = await _context.Animes
            .Include(x => x.AnimeTags)
            .FirstOrDefaultAsync(x => x.ExternalId == animeId);
        if(anime is null) return;
        if (anime.AnimeTags is null)
        {
            anime.AnimeTags = new List<AnimeTag>() {new AnimeTag() {AnimeId = anime.Id, TagId = tagEntity.Id}};
        }
        else
        {
            anime.AnimeTags.Add(new AnimeTag() {AnimeId = anime.Id, TagId = tagEntity.Id});

        }
        _context.Animes.Update(anime);
    }
}