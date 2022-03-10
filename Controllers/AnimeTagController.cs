using aninja_tags_service.Commands;
using aninja_tags_service.Dtos;
using aninja_tags_service.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace aninja_tags_service.Controllers;

[Route("api/t/anime/{animeId}/tag")]
[ApiController]
public class AnimeTagController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AnimeTagController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetTagsForAnime(int animeId)
    {
        var request = new GetTagsForAnimeQuery() {AnimeId = animeId};
        var tags = await _mediator.Send(request);
        if (tags is null) return NotFound();
        return Ok(_mapper.Map<IEnumerable<TagDto>>(tags));
    }

    [HttpPut("{tagId}")]
    public async Task<ActionResult<TagDto>> AddTagToAnime(int animeId, int tagId)
    {
        var request = new AddAnimeTagCommand() {AnimeId = animeId, TagId = tagId};
        var tag = await _mediator.Send(request);
        if (tag is null) return NotFound();
        return Ok(_mapper.Map<TagDto>(tag));
    }

    [HttpDelete("{tagId}")]
    public async Task<ActionResult<TagDto>> RemoveAnimeTag(int animeId, int tagId)
    {
        var request = new RemoveAnimeTagCommand()
        {
            AnimeId = animeId,
            TagId = tagId
        };
        var result = await _mediator.Send(request);
        if (result is null) return NotFound();
        return Ok(_mapper.Map<TagDto>(result));
    }

}