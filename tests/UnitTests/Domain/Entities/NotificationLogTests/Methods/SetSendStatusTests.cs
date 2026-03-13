using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace UnitTests.Domain.Entities.NotificationLogTests.Methods;

public class SetSendStatusTests
{
    [Fact]
    public void SetSendStatus_UpdatesStatusAndErrorAndUpdatedAt()
    {
        // Arrange
        var notificationId = "notif-1";
        var target = new NotificationTarget { Channel = NotificationChannel.Email, Target = "user@example.com" };
        var log = new NotificationLog(notificationId, target);
        var before = DateTime.UtcNow;

        // Act
        log.SetSendStatus(NotificationStatus.Sent, "ok");
        var after = DateTime.UtcNow;

        // Assert
        Assert.Equal(NotificationStatus.Sent, log.Status);
        Assert.Equal("ok", log.Error);
        Assert.NotNull(log.UpdatedAt);
        Assert.InRange(log.UpdatedAt.Value, before.AddSeconds(-1), after.AddSeconds(1));
    }

    [Fact]
    public void SetSendStatus_WithNullError_SetsStatusAndClearsError()
    {
        // Arrange
        var notificationId = "notif-2";
        var target = new NotificationTarget { Channel = NotificationChannel.Webhook, Target = "http://cb" };
        var log = new NotificationLog(notificationId, target);

        // Act
        log.SetSendStatus(NotificationStatus.Failed);

        // Assert
        Assert.Equal(NotificationStatus.Failed, log.Status);
        Assert.Null(log.Error);
        Assert.NotNull(log.UpdatedAt);
    }
}
