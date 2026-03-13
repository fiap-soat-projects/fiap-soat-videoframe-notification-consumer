using Adapter.Messages;
using Adapter.Messages.Validators;
using Domain.Enums;
using Domain.ValueObjects;

namespace UnitTests.Adapter.Messages.Validators.NotificationMessageValidatorTests.Methods;

public class ValidateTests
{
    [Fact]
    public void When_MessageIsNull_Then_ThrowArgumentNullException()
    {
        // Arrange
        NotificationMessage? message = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => NotificationMessageValidator.Validate(message!));
    }

    [Fact]
    public void When_TypeIsSuccessAndFileUrlIsEmpty_Then_ThrowArgumentException()
    {
        // Arrange
        var message = new NotificationMessage
        {
            EditId = "edit",
            UserId = "user",
            UserName = "name",
            FileUrl = string.Empty,
            Type = NotificationType.Success,
            NotificationTargets = [new NotificationTarget { Channel = NotificationChannel.Webhook, Target = "t" }]
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => NotificationMessageValidator.Validate(message));
    }

    [Fact]
    public void When_NoTargetsDefined_Then_ThrowInvalidOperationException()
    {
        // Arrange
        var message = new NotificationMessage
        {
            EditId = "edit",
            UserId = "user",
            UserName = "name",
            FileUrl = "file",
            Type = NotificationType.Error,
            NotificationTargets = []
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => NotificationMessageValidator.Validate(message));
    }

    [Fact]
    public void When_TargetsContainInvalidTarget_Then_ThrowInvalidOperationException()
    {
        // Arrange
        var message = new NotificationMessage
        {
            EditId = "edit",
            UserId = "user",
            UserName = "name",
            FileUrl = "file",
            Type = NotificationType.Error,
            NotificationTargets = [new NotificationTarget { Channel = NotificationChannel.Webhook, Target = string.Empty }]
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => NotificationMessageValidator.Validate(message));
    }

    [Fact]
    public void When_ValidMessage_Then_DoesNotThrow()
    {
        // Arrange
        var message = new NotificationMessage
        {
            EditId = "edit",
            UserId = "user",
            UserName = "name",
            FileUrl = "file",
            Type = NotificationType.Success,
            NotificationTargets = [new NotificationTarget { Channel = NotificationChannel.Webhook, Target = "t" }]
        };

        // Act & Assert
        NotificationMessageValidator.Validate(message);
    }
}
