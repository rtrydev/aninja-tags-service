using aninja_anime_service;
using aninja_tags_service.Queries;
using aninja_tags_service.Repositories;
using AutoMapper;
using Grpc.Core;
using MediatR;

namespace aninja_tags_service.SyncDataServices;

public class GrpcTagsService : GrpcTag.GrpcTagBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GrpcTagsService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<AnimeResponse> GetAllAnimeWithTags(GetAllWithTagsRequest request, ServerCallContext context)
    {
        var response = new AnimeResponse();
        var query = new GetAnimesWithTagsQuery() {tagIds = request.TagId.ToArray()};
        var animes = await _mediator.Send(query);
        foreach (var anime in animes)
        {
            response.Anime.Add(_mapper.Map<GrpcAnimeModel>(anime));
        }

        return response;
    }
}