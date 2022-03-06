using aninja_tags_service.Commands;
using aninja_tags_service.Dtos;
using aninja_tags_service.Models;
using aninja_tags_service.Queries;
using aninja_tags_service.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace aninja_tags_service.Controllers;

[Route("api/t/tag")]
[ApiController]
public class TagController : ControllerBase
{
    private IMediator _mediator;
    private IMapper _mapper;

    public TagController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetAllTags()
    {
        var query = new GetAllTagsQuery();
        var tags = await _mediator.Send(query);
        return Ok(_mapper.Map<IEnumerable<TagDto>>(tags));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TagDetailsDto>> GetTag(int id)
    {
        var query = new GetTagByIdQuery() {Id = id};
        var tag = await _mediator.Send(query);
        return Ok(_mapper.Map<TagDetailsDto>(tag));
    }

    [HttpPost]
    public async Task<ActionResult<TagDetailsDto>> AddTag([FromBody] TagWriteDto tag)
    {
        var request = _mapper.Map<AddTagCommand>(tag);
        var result = await _mediator.Send(request);
        return Ok(_mapper.Map<TagDetailsDto>(result));
    }
}