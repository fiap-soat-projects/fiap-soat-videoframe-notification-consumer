using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.UseCases;
using Domain.ValueObjects;
using NSubstitute;

namespace UnitTests.Domain.UseCases.NotificationUseCaseTests.Methods;

public class CreateAsyncTests
{
    [Fact]
    public async Task When_CreateAsync_Then_CallsRepositoryAndReturnsCreatedNotification()
    {
        // Arrange
        var repository = Substitute.For<INotificationRepository>();
        var notificationTargets = new List<NotificationTarget>
        {
            new() { Channel = NotificationChannel.Email, Target = "user@example.com" }
        };

        var input = new Notification("edit-1", "user-1", "User 1", "http://file", NotificationType.Success, null, notificationTargets);
        var created = new Notification("id-1", input.CreatedAt, null, input.EditId, input.UserId, input.UserName, input.FileUrl, input.Type, input.Error, input.NotificationTargets);

        repository.CreateAsync(input, CancellationToken.None).Returns(Task.FromResult(created));

        var useCase = new NotificationUseCase(repository);

        // Act
        var result = await useCase.CreateAsync(input, CancellationToken.None);

        // Assert
        await repository.Received(1).CreateAsync(input, CancellationToken.None);
        Assert.Equal(created, result);
    }
}
