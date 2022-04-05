namespace CommandService.Api.AsyncDataServices;

public class MessageBusSubscriber : BackgroundService
{
    private readonly IConfiguration _config;
    private readonly IEventProcessor _eventProcessor;
    private readonly ILogger<MessageBusSubscriber> _logger;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;
    private const string exchangeName = "trigger";

    public MessageBusSubscriber(IConfiguration config, IEventProcessor eventProcessor, ILogger<MessageBusSubscriber> logger)
    {
        _config = config;
        _eventProcessor = eventProcessor;
        _logger = logger;
        InitializeRabbitMQ();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (ModuleHandle, deliveryEvent) =>
        {
            _logger.LogInformation("---> Event Received.");
            var body = deliveryEvent.Body;
            var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

            _eventProcessor.ProcessEvent(notificationMessage);
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        return Task.CompletedTask;
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
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(
                queue: _queueName,
                exchange: exchangeName,
                routingKey: string.Empty
            );
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _logger.LogInformation("---> Listening to Message Bus.");
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

    public override void Dispose()
    {
        _logger.LogInformation("MessageBus Subscriber Disposed.");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
