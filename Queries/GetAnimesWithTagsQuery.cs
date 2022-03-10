using aninja_tags_service.Models;
using MediatR;

namespace aninja_tags_service.Queries;

public class GetAnimesWithTagsQuery : IRequest<IEnumerable<Anime>>
{
    public IEnumerable<int>? tagIds { get; set; }
}