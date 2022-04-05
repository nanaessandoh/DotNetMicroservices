namespace PlatformService.Api.SyncDataServices;

public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
{
    private readonly PlatformDataProvider _platformDataProvider;
    private readonly IMapper _mapper;

    public GrpcPlatformService(PlatformDataProvider platformDataProvider, IMapper mapper)
    {
        _platformDataProvider = platformDataProvider;
        _mapper = mapper;
    }

    public override async Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
    {
        var response = new PlatformResponse();
        var platforms = await _platformDataProvider.GetAll();

        foreach(var platform in platforms)
        {
            response.Platform.Add(_mapper.Map<GrpcPlatformModel>(platform));
        }

        return response;
    }
}
