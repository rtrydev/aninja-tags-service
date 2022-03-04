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

    public async Task<Tag> GetTag(int tagId)
    {
        return await _context.Tags.FirstOrDefaultAsync(x => x.Id == tagId);
    }

    public async Task<IEnumerable<Tag>?> GetTagsForAnime(int animeId)
    {
        var animes = await _context.Animes.Include(x => x.AnimeTags)
            .FirstOrDefaultAsync(x => x.Id == animeId);
        var tags = animes.AnimeTags.Select(x => x.Tag);
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

    public async Task AddTag(Tag tag)
    {
        await _context.Tags.AddAsync(tag);
    }

    public async Task AddAnimeTag(int animeId, Tag tag)
    {
        var tagEntity = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tag.Id);
        if(tagEntity is null) return;
        var anime = await _context.Animes
            .Include(x => x.AnimeTags)
            .FirstOrDefaultAsync(x => x.Id == animeId);
        if(anime is null) return;
        if (anime.AnimeTags.FirstOrDefault() is null)
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