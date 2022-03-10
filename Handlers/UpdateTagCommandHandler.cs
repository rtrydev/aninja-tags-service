using aninja_tags_service.Commands;
using aninja_tags_service.Models;
using aninja_tags_service.Repositories;
using AutoMapper;
using MediatR;

namespace aninja_tags_service.Handlers;

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, Tag>
{
    private ITagRepository _repository;
    private IMapper _mapper;

    public UpdateTagCommandHandler(ITagRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Tag> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Tag>(request);
        var result = await _repository.UpdateTag(item);
        await _repository.SaveChangesAsync();
        return await Task.FromResult(result);
    }
}