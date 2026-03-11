using Infrastructure.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Consumer;

[ExcludeFromCodeCoverage]
public class Worker : BackgroundService
{
    private readonly IKafkaService _kafkaService;
    private readonly ILogger<Worker> _logger;

    public Worker(IKafkaService kafkaService, ILogger<Worker> logger)
    {
        _kafkaService = kafkaService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        const string VIDEOFRAME_NOTIFICATION_TOPIC_NAME = "notification-consumer";

        _kafkaService.Subscribe(VIDEOFRAME_NOTIFICATION_TOPIC_NAME);

        while (stoppingToken.IsCancellationRequested is false)
        {
            var consumeResult = _kafkaService.Consume(stoppingToken);

            var message = consumeResult.Message;

            // TODO: Add implementation here

            _kafkaService.Commit();
        }
    }
}
