using aninja_tags_service.Commands;
using aninja_tags_service.Models;
using aninja_tags_service.Repositories;
using AutoMapper;
using MediatR;

namespace aninja_tags_service.Handlers;

public class AddAnimeTagCommandHandler : IRequestHandler<AddAnimeTagCommand, Tag?>
{
    private ITagRepository _tagRepository;

    public AddAnimeTagCommandHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<Tag?> Handle(AddAnimeTagCommand request, CancellationToken cancellationToken)
    {
        var tagEntity = await _tagRepository.GetTag(request.TagId);
        if(tagEntity is null) return null;
        var anime = await _tagRepository.GetAnime(request.AnimeId);
        if(anime is null) return null;
        if (anime.AnimeTags is null)
        {
            anime.AnimeTags = new List<AnimeTag>() {new AnimeTag() {AnimeId = anime.Id, TagId = tagEntity.Id}};
        }
        else
        {
            anime.AnimeTags.Add(new AnimeTag() {AnimeId = anime.Id, TagId = tagEntity.Id});
        }

        await _tagRepository.UpdateAnime(anime);
        await _tagRepository.SaveChangesAsync();

        return new Tag() {Id = tagEntity.Id, Name = tagEntity.Name};
    }
}