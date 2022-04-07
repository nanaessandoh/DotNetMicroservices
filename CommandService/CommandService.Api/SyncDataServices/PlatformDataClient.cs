namespace CommandService.Api.SyncDataServices;

public class PlatformDataClient : IPlatformDataClient
{
    private readonly ILogger<PlatformDataClient> _logger;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;

    public PlatformDataClient(ILogger<PlatformDataClient> logger, IConfiguration config, IMapper mapper)
    {
        _logger = logger;
        _config = config;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Platform>> ReturnAllPlatforms()
    {
        _logger.LogInformation($"---> Calling gRPC Service {_config["gRPCPlatform"]}");
        var channel = GrpcChannel.ForAddress(_config["gRPCPlatform"]);
        var client = new GrpcPlatform.GrpcPlatformClient(channel);
        var request = new GetAllRequest();

        try
        {
            var reply = await client.GetAllPlatformsAsync(request);
            return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
        }
        catch (Exception ex)
        {
            _logger.LogError("---> Could not call gRPC Server", ex);
            return Enumerable.Empty<Platform>();
        }
    }
}
