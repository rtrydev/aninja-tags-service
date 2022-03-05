using aninja_tags_service.Dtos;
using aninja_tags_service.Models;
using aninja_tags_service.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace aninja_tags_service.Controllers;

[Route("api/c/tag")]
[ApiController]
public class TagController : ControllerBase
{
    private ITagRepository _tagRepository;
    private IMapper _mapper;

    public TagController(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetAllTags()
    {
        var tags = await _tagRepository.GetAllTags();
        return Ok(_mapper.Map<IEnumerable<TagDto>>(tags));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TagDetailsDto>> GetTag(int id)
    {
        var tag = await _tagRepository.GetTag(id);
        return Ok(_mapper.Map<TagDetailsDto>(tag));
    }

    [HttpPost]
    public async Task<ActionResult> AddTag([FromBody] TagWriteDto tag)
    {
        await _tagRepository.AddTag(_mapper.Map<Tag>(tag));
        await _tagRepository.SaveChangesAsync();
        return Ok();
    }
}