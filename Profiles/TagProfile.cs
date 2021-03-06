using aninja_tags_service.Commands;
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
        CreateMap<TagWriteDto, AddTagCommand>();
        CreateMap<AddTagCommand, Tag>();
        CreateMap<TagWriteDto, UpdateTagCommand>();
        CreateMap<UpdateTagCommand, Tag>();
    }
}