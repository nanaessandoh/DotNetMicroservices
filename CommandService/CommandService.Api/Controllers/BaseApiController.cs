namespace CommandService.Api.Controllers;

[ApiController]
[Route("api/c/[controller]")]
public abstract class BaseApiController<TController> : ControllerBase
where TController : BaseApiController<TController>
{
    private readonly ILogger<TController> _logger;

    public BaseApiController(ILogger<TController> logger)
    {
        _logger = logger;
    }

    public IActionResult Try(Func<IActionResult> function)
    {
        try
        {
            return function();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured in the API");
            throw;
        }
    }

    public async Task<IActionResult> TryAsync(Func<Task<IActionResult>> asyncFunction)
    {
        try
        {
            return await asyncFunction();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured in the API");
            throw;
        }
    }
}

