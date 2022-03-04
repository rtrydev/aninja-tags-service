using aninja_tags_service.Dtos;
using aninja_tags_service.Models;
using aninja_tags_service.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace aninja_tags_service.Controllers;

[Route("api/anime/{animeId}/tag")]
[ApiController]
public class AnimeTagController : ControllerBase
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public AnimeTagController(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetTagsForAnime(int animeId)
    {
        await _tagRepository.AddAnimeTag(1, new Tag() {Id = 1, Name = "Horror"});
        var tags = await _tagRepository.GetTagsForAnime(animeId);
        return Ok(_mapper.Map<IEnumerable<TagDto>>(tags));
    }

    [HttpPut]
    public async Task<ActionResult> AddTagToAnime(int animeId, [FromBody] TagWriteDto tag)
    {
        await _tagRepository.AddAnimeTag(animeId, _mapper.Map<Tag>(tag));
        await _tagRepository.SaveChangesAsync();
        return Ok();
    }

}