using aninja_tags_service.Commands;
using aninja_tags_service.Models;
using aninja_tags_service.Repositories;
using AutoMapper;
using MediatR;

namespace aninja_tags_service.Handlers;

public class RemoveAnimeTagCommandHandler : IRequestHandler<RemoveAnimeTagCommand, Tag?>
{
    private ITagRepository _tagRepository;
    private IMapper _mapper;

    public RemoveAnimeTagCommandHandler(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<Tag?> Handle(RemoveAnimeTagCommand request, CancellationToken cancellationToken)
    {
        var anime = await _tagRepository.GetAnime(request.AnimeId);
        if (anime is null) return null;
        if(anime.AnimeTags is null) return null;

        var animeTag = anime.AnimeTags.FirstOrDefault(x => x.TagId == request.TagId);
        if (animeTag is null) return null;
        anime.AnimeTags.Remove(animeTag);
        var result = await _tagRepository.UpdateAnime(anime);
        await _tagRepository.SaveChangesAsync();

        return await _tagRepository.GetTag(animeTag.TagId);
    }
}