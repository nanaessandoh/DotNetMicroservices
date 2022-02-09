namespace CommandService.Api.Controllers;

public class PlatformsController : BaseApiController<PlatformsController>
{
    public PlatformsController(
        ILogger<PlatformsController> logger
    ): base(logger)
    {

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
