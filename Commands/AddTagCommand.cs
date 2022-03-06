using aninja_tags_service.Models;
using MediatR;

namespace aninja_tags_service.Commands;

public class AddTagCommand : IRequest<Tag>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}