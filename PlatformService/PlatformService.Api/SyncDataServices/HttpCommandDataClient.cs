namespace PlatformService.Api.SyncDataServices;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILogger<HttpCommandDataClient> _logger;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration config, ILogger<HttpCommandDataClient> logger)
    {
        _httpClient = httpClient;
        _config = config;
        _logger = logger;
    }
    public async Task SendPlatformToCommand(PlatformViewModel platform)
    {
        try
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_config["CommandService"]}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("---> Sync POST to CommandService was OK!");
            }
            else
            {
                _logger.LogError($"---> Sync POST to CommandService was NOT OK! Response StatusCode: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"---> Could not sent synchronously: {ex.Message}");
        }
    }
}
