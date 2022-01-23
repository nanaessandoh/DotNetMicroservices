namespace PlatformService.Controllers;

public class PlatformsController : BaseApiController<PlatformsController>
{
    private readonly IPlatformDataProvider _platformDataProvider;
    private readonly IMapper _mapper;

    public PlatformsController(
        ILogger<PlatformsController> logger,
        IPlatformDataProvider platformDataProvider,
        IMapper mapper)
    : base (logger)
    {
        _platformDataProvider = platformDataProvider;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlatforms()
    {
        return await TryAsync(async () =>
        {
            var plaforms = await _platformDataProvider.GetAll();
            return Ok(_mapper.Map<IEnumerable<PlatformViewModel>>(plaforms));
            // OR
            //return Ok(plaforms.Adapt<PlatformViewModel>());
        });
    }

    [HttpGet("{id}", Name = "GetPlatform")]
    public async Task<IActionResult> GetPlatform(int id)
    {
        return await TryAsync(async () =>
        {
            var platform = await _platformDataProvider.Get(id);
            return Ok(_mapper.Map<PlatformViewModel>(platform));
            // OR
            //return Ok(platform.Adapt<PlatformViewModel>());
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlatform([FromBody] PlatformCreateModel platform)
    {
        return await TryAsync(async () =>
        {
            var newPlatform = _mapper.Map<Platform>(platform);
            newPlatform = await _platformDataProvider.Add(newPlatform);
            return CreatedAtRoute(
                    nameof(GetPlatform),
                    new { Id = newPlatform.Id},
                    _mapper.Map<PlatformViewModel>(newPlatform)
                );
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
