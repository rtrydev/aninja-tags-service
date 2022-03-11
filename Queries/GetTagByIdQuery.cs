using aninja_tags_service.Models;
using MediatR;

namespace aninja_tags_service.Queries;

public class GetTagByIdQuery : IRequest<Tag?>
{
    public int Id { get; set; }
}