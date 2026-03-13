using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.UseCases;
using Domain.ValueObjects;
using NSubstitute;

namespace UnitTests.Domain.UseCases.NotificationLogUseCaseTests.Methods;

public class CreateAsyncTests
{
    [Fact]
    public async Task When_CreateAsync_Then_CallsRepositoryAndReturnsCreatedLog()
    {
        // Arrange
        var repository = Substitute.For<INotificationLogRepository>();
        var target = new NotificationTarget { Channel = NotificationChannel.Email, Target = "user@example.com" };
        var input = new NotificationLog("notification-1", target);
        var created = new NotificationLog("log-1", input.CreatedAt, null, input.NotificationId, input.Target, input.Status, input.Error);

        repository.CreateAsync(input, CancellationToken.None).Returns(Task.FromResult(created));

        var useCase = new NotificationLogUseCase(repository);

        // Act
        var result = await useCase.CreateAsync(input, CancellationToken.None);

        // Assert
        await repository.Received(1).CreateAsync(input, CancellationToken.None);
        Assert.Same(created, result);
    }
}
