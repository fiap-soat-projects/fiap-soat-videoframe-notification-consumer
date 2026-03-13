using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace UnitTests.Domain.Entities.NotificationLogTests.Constructors;

public class ConstructorsTests
{
    [Fact]
    public void Constructor_WithRequiredFields_SetsDefaults()
    {
        // Arrange
        var notificationId = "notif-1";
        var target = new NotificationTarget { Channel = NotificationChannel.Email, Target = "user@example.com" };

        // Act
        var before = DateTime.UtcNow;
        var log = new NotificationLog(notificationId, target);
        var after = DateTime.UtcNow;

        // Assert
        Assert.Null(log.Id);
        Assert.InRange(log.CreatedAt, before.AddSeconds(-1), after.AddSeconds(1));
        Assert.Equal(notificationId, log.NotificationId);
        Assert.Equal(target, log.Target);
        Assert.Equal(NotificationStatus.Pending, log.Status);
        Assert.Null(log.Error);
    }

    [Fact]
    public void Constructor_WithAllFields_SetsProperties()
    {
        // Arrange
        var id = "log-1";
        var createdAt = new DateTime(2020, 2, 1, 0, 0, 0, DateTimeKind.Utc);
        var updatedAt = new DateTime(2020, 2, 2, 0, 0, 0, DateTimeKind.Utc);
        var notificationId = "notif-2";
        var target = new NotificationTarget { Channel = NotificationChannel.Webhook, Target = "http://cb" };
        var status = NotificationStatus.Sent;
        var error = "no error";

        // Act
        var log = new NotificationLog(id, createdAt, updatedAt, notificationId, target, status, error);

        // Assert
        Assert.Equal(id, log.Id);
        Assert.Equal(createdAt, log.CreatedAt);
        Assert.Equal(updatedAt, log.UpdatedAt);
        Assert.Equal(notificationId, log.NotificationId);
        Assert.Equal(target, log.Target);
        Assert.Equal(status, log.Status);
        Assert.Equal(error, log.Error);
    }
}
