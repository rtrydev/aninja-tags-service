using aninja_tags_service.Dtos;
using aninja_tags_service.Models;
using AutoMapper;

namespace aninja_tags_service.Profiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDto>();
        CreateMap<TagWriteDto, Tag>();
        CreateMap<Tag, TagDetailsDto>();
    }
}