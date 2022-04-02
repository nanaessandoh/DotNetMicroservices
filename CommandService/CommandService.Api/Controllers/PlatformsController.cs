namespace CommandService.Api.Controllers;

public class PlatformsController : BaseApiController<PlatformsController>
{
    private readonly ILogger<PlatformsController> _logger;
    private readonly ICommandDataProvider _commandDataProvider;
    private readonly IMapper _mapper;

    public PlatformsController(
        ILogger<PlatformsController> logger,
        ICommandDataProvider commandDataProvider,
        IMapper mapper
    ): base(logger)
    {
        _logger = logger;
        _commandDataProvider = commandDataProvider;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlatforms()
    {
        _logger.LogInformation("---> Getting Platforms from Command Service");
        return await TryAsync(async () =>
        {
            var plaforms = await _commandDataProvider.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformViewModel>>(plaforms));
        });
    }

    [HttpPost]
    public IActionResult TestInboundConnection()
    {
        Console.WriteLine(" --> Inbound POST - CommandService");
        return Try(() =>
        {
            return Ok();
        });
    }
}
