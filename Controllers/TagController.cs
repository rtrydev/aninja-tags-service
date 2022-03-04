using aninja_tags_service.Dtos;
using aninja_tags_service.Models;
using aninja_tags_service.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace aninja_tags_service.Controllers;

[Route("api/anime/{animeId}/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public TagController(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagReadDto>>> GetTagsForAnime(int animeId)
    {
        var tags = await _tagRepository.GetTagsForAnime(animeId);
        return Ok(_mapper.Map<IEnumerable<TagReadDto>>(tags));
    }

}