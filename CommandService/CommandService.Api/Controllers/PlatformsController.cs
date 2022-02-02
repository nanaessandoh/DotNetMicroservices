namespace CommandService.Api.Controllers;

public class PlatformsController : BaseApiController<PlatformsController>
{
    public PlatformsController(
        ILogger<PlatformsController> logger
    ): base(logger)
    {
        
    }

    [HttpPost]
    public async Task<IActionResult> TestInboundConnection()
    {
        return await TryAsync(async () =>
        {
            return Ok("Inbound test from Platforms Controller");
        });
    }

}
