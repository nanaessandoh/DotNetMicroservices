namespace PlatformService.Controllers;

public class PlatformsController : BaseApiController<PlatformsController>
{
    private readonly IPlatformDataProvider _platformDataProvider;
    private readonly IMapper _mapper;
    private readonly ICommandDataClient _commandClient;

    public PlatformsController(
        ILogger<PlatformsController> logger,
        IPlatformDataProvider platformDataProvider,
        IMapper mapper,
        ICommandDataClient commandClient)
    : base (logger)
    {
        _platformDataProvider = platformDataProvider;
        _mapper = mapper;
        _commandClient = commandClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlatforms()
    {
        return await TryAsync(async () =>
        {
            var plaforms = await _platformDataProvider.GetAll();
            return Ok(_mapper.Map<IEnumerable<PlatformViewModel>>(plaforms));
        });
    }

    [HttpGet("{id}", Name = "GetPlatform")]
    public async Task<IActionResult> GetPlatform(int id)
    {
        return await TryAsync(async () =>
        {
            var platform = await _platformDataProvider.Get(id);
            return Ok(_mapper.Map<PlatformViewModel>(platform));
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlatform([FromBody] PlatformCreateModel platform)
    {
        return await TryAsync(async () =>
        {
            var newPlatform = _mapper.Map<Platform>(platform);
            newPlatform = await _platformDataProvider.Add(newPlatform);
            var viewModel = _mapper.Map<PlatformViewModel>(newPlatform);
            await _commandClient.SendPlatformToCommand(viewModel);

            return CreatedAtRoute(nameof(GetPlatform), new {id = newPlatform.Id}, viewModel);
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlatform(int id)
    {
        return await TryAsync(async () =>
        {
            await _platformDataProvider.Delete(id);
            return Ok();
        });
    }
}
