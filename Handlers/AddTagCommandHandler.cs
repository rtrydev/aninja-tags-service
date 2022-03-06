using aninja_tags_service.Commands;
using aninja_tags_service.Models;
using aninja_tags_service.Repositories;
using AutoMapper;
using MediatR;

namespace aninja_tags_service.Handlers;

public class AddTagCommandHandler : IRequestHandler<AddTagCommand, Tag>
{
    private IMapper _mapper;
    private ITagRepository _tagRepository;

    public AddTagCommandHandler(IMapper mapper, ITagRepository tagRepository)
    {
        _mapper = mapper;
        _tagRepository = tagRepository;
    }

    public async Task<Tag> Handle(AddTagCommand request, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Tag>(request);
        var tag = await _tagRepository.CreateTag(item);
        await _tagRepository.SaveChangesAsync();
        return tag;
    }
}