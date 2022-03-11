using aninja_tags_service.Models;
using aninja_tags_service.Queries;
using aninja_tags_service.Repositories;
using MediatR;

namespace aninja_tags_service.Handlers;

public class GetTagsForAnimeQueryHandler : IRequestHandler<GetTagsForAnimeQuery, IEnumerable<Tag>?>
{
    private ITagRepository _tagRepository;

    public GetTagsForAnimeQueryHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<IEnumerable<Tag>?> Handle(GetTagsForAnimeQuery request, CancellationToken cancellationToken)
    {
        var anime = await _tagRepository.GetAnime(request.AnimeId);
        if (anime is null) return null;
        if (anime.AnimeTags is null) return null;

        var animeTags = anime.AnimeTags.Select(x => x.TagId);

        var tags = await _tagRepository.GetAllTags();
        
        return tags.Where(x => animeTags.Contains(x.Id));
    }
}