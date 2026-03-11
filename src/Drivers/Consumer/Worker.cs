using Adapter.Applications.Interfaces;
using Adapter.Messages;
using Infrastructure.Providers;
using Infrastructure.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Consumer;

[ExcludeFromCodeCoverage]
public class Worker : BackgroundService
{
    private readonly IKafkaService _kafkaService;
    private readonly INotificationApplication _notificationApplication;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<Worker> _logger;

    public Worker(
        IKafkaService kafkaService,
        INotificationApplication notificationApplication,
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<Worker> logger)
    {
        _kafkaService = kafkaService;
        _notificationApplication = notificationApplication;
        _hostApplicationLifetime = hostApplicationLifetime;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.UtcNow);

        _kafkaService.Subscribe(StaticEnvironmentVariableProvider.NotificationTopicName);

        while (stoppingToken.IsCancellationRequested is false)
        {
            var notificationMessage = _kafkaService.Consume<NotificationMessage>(stoppingToken);

            try
            {
                await _notificationApplication.NotifyAsync(notificationMessage, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error message: {Message}", ex.Message);

                _kafkaService.Dispose();

                _hostApplicationLifetime.StopApplication();
            }

            _kafkaService.Commit();
        }
    }
}
