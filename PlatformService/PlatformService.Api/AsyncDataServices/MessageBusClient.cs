

namespace PlatformService.Api.AsyncDataServices;

public class MessageBusClient : IMessageBusClient, IDisposable
{
    private readonly IConfiguration _config;
    private readonly ILogger<MessageBusClient> _logger;
    private IConnection _connection;
    private IModel _channel;
    private const string exchangeName = "trigger";

    public MessageBusClient(IConfiguration config, ILogger<MessageBusClient> logger)
    {
        _config = config;
        _logger = logger;
        InitializeRabbitMQ();
    }

    public void PublishNewPlatform(PlatformPublishModel publishedPlatform)
    {
        try
        {
            var message = JsonSerializer.Serialize(publishedPlatform);

            if (_connection.IsOpen)
            {
                _logger.LogInformation("---> RabbitMQ Connection Open, Sending Message");
                SendMessage(message);
            }
            else
            {
                _logger.LogInformation("---> RabbitMQ Connection Closed, Not Sending");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("---> RabbitMQ Connection Closed, Not Sending", ex);
        }
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(
            exchange: exchangeName,
            routingKey: string.Empty,
            basicProperties: null,
            body: body
        );
        _logger.LogInformation($"---> Message Sent. Message: {message}");
    }

    private void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _config["RabbitMQHost"],
            Port = int.Parse(_config["RabbitMQPort"])
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _logger.LogInformation("---> Initialized Message Bus.");

        }
        catch(Exception ex)
        {
            _logger.LogError("---> Could not connect to the Message Bus", ex);
        }
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs evnt)
    {
        _logger.LogInformation("---> RabbitMQ connection shutdown.");
    }

    public void Dispose()
    {
        _logger.LogInformation("MessageBus Client Disposed.");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
