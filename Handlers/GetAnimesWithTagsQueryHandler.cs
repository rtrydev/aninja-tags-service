using aninja_tags_service.Models;
using aninja_tags_service.Queries;
using aninja_tags_service.Repositories;
using MediatR;

namespace aninja_tags_service.Handlers;

public class GetAnimesWithTagsQueryHandler : IRequestHandler<GetAnimesWithTagsQuery, IEnumerable<Anime>?>
{
    private ITagRepository _tagRepository;

    public GetAnimesWithTagsQueryHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<IEnumerable<Anime>?> Handle(GetAnimesWithTagsQuery request, CancellationToken cancellationToken)
    {
        var animes = await _tagRepository.GetAllAnimes();
        if (request.tagIds is not null)
        {
            animes = animes.Where(x => x.AnimeTags is not null);
            animes = animes.Where(x => request.tagIds.All(y => x.AnimeTags!.Any(z => z.TagId == y)));
        }

        return animes.Select(x => new Anime(){ExternalId = x.ExternalId, TranslatedTitle = x.TranslatedTitle});

    }
}