using aninja_tags_service.Models;
using MediatR;

namespace aninja_tags_service.Queries;

public class GetTagsForAnimeQuery : IRequest<IEnumerable<Tag>?>
{
    public int AnimeId { get; set; }
}