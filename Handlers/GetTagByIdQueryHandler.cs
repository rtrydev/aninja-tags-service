using aninja_tags_service.Models;
using aninja_tags_service.Queries;
using aninja_tags_service.Repositories;
using MediatR;

namespace aninja_tags_service.Handlers;

public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, Tag>
{
    private ITagRepository _tagRepository;

    public GetTagByIdQueryHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<Tag> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _tagRepository.GetTag(request.Id);
        return item;
    }
}