using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.UseCases;
using Domain.ValueObjects;
using NSubstitute;

namespace UnitTests.Domain.UseCases.NotificationLogUseCaseTests.Methods;

public class UpdateStatusAsyncTests
{
    [Fact]
    public async Task When_UpdateStatusAsync_Then_CallsRepositoryUpdateStatus()
    {
        // Arrange
        var repository = Substitute.For<INotificationLogRepository>();
        var target = new NotificationTarget { Channel = NotificationChannel.Webhook, Target = "http://callback" };
        var log = new NotificationLog("notification-1", target);

        var useCase = new NotificationLogUseCase(repository);

        // Act
        await useCase.UpdateStatusAsync(log, CancellationToken.None);

        // Assert
        await repository.Received(1).UpdateStatusAsync(log, CancellationToken.None);
    }
}
