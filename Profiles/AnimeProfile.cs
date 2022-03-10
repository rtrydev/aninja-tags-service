using aninja_tags_service.Dtos;
using aninja_tags_service.Models;
using AutoMapper;

namespace aninja_tags_service.Profiles;

public class AnimeProfile : Profile
{
    public AnimeProfile()
    {
        CreateMap<AnimePublishedDto, Anime>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
        CreateMap<aninja_anime_service.GrpcAnimeModel, Anime>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.AnimeId))
            .ForMember(dest => dest.AnimeTags, opt => opt.Ignore());
        CreateMap<Anime, GrpcAnimeModel>()
            .ForMember(dest => dest.AnimeId, opt => opt.MapFrom(src => src.ExternalId));

    }
}