using aninja_tags_service.Dtos;
using aninja_tags_service.Models;
using AutoMapper;

namespace aninja_tags_service.Profiles;

public class AnimeProfile : Profile
{
    public AnimeProfile()
    {
        CreateMap<AnimePublishedDto, Anime>();
    }
}