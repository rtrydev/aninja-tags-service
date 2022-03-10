using aninja_tags_service.Models;
using MediatR;

namespace aninja_tags_service.Commands;

public class AddAnimeTagCommand : IRequest<Tag?>
{
    public int AnimeId { get; set; }
    public int TagId { get; set; }
}