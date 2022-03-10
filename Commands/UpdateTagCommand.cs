using aninja_tags_service.Models;
using MediatR;

namespace aninja_tags_service.Commands;

public class UpdateTagCommand : IRequest<Tag>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}