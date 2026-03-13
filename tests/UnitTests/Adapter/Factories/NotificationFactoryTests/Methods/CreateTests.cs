using Adapter.Factories;
using Adapter.Messages;
using Domain.Enums;
using Domain.ValueObjects;

namespace UnitTests.Adapter.Factories.NotificationFactoryTests.Methods;

public class CreateTests
{
    [Fact]
    public void When_CreateCalled_Then_ReturnsNotificationWithSameProperties()
    {
        // Arrange
        var message = new NotificationMessage
        {
            EditId = "edit",
            UserId = "user",
            UserName = "name",
            FileUrl = "file",
            Type = NotificationType.Error,
            Error = "err",
            NotificationTargets = [new NotificationTarget { Channel = NotificationChannel.Webhook, Target = "t" }]
        };

        // Act
        var notification = NotificationFactory.Create(message);

        // Assert
        Assert.Equal(message.EditId, notification.EditId);
        Assert.Equal(message.UserId, notification.UserId);
        Assert.Equal(message.UserName, notification.UserName);
        Assert.Equal(message.FileUrl, notification.FileUrl);
        Assert.Equal(message.Type, notification.Type);
        Assert.Equal(message.Error, notification.Error);
        Assert.Equal(message.NotificationTargets, notification.NotificationTargets);
    }
}
