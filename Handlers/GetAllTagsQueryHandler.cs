using aninja_tags_service.Models;
using aninja_tags_service.Queries;
using aninja_tags_service.Repositories;
using MediatR;

namespace aninja_tags_service.Handlers;

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, IEnumerable<Tag>>
{
    private ITagRepository _tagRepository;

    public GetAllTagsQueryHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<IEnumerable<Tag>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var items = await _tagRepository.GetAllTags();
        var result = items; // here implement some queries later

        return result;
    }
}