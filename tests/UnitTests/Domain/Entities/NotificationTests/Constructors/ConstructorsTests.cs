using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace UnitTests.Domain.Entities.NotificationTests.Constructors;

public class ConstructorsTests
{
    [Fact]
    public void Constructor_WithRequiredFields_SetsProperties()
    {
        // Arrange
        var editId = "edit-1";
        var userId = "user-1";
        var userName = "User One";
        var fileUrl = "http://file.url";
        var type = NotificationType.Success;
        var error = (string?)null;
        var targets = new List<NotificationTarget>
        {
            new NotificationTarget { Channel = NotificationChannel.Webhook, Target = "http://callback" }
        };

        // Act
        var before = DateTime.UtcNow;
        var notification = new Notification(editId, userId, userName, fileUrl, type, error, targets);
        var after = DateTime.UtcNow;

        // Assert
        Assert.Null(notification.Id);
        Assert.InRange(notification.CreatedAt, before.AddSeconds(-1), after.AddSeconds(1));
        Assert.Equal(editId, notification.EditId);
        Assert.Equal(userId, notification.UserId);
        Assert.Equal(userName, notification.UserName);
        Assert.Equal(fileUrl, notification.FileUrl);
        Assert.Equal(type, notification.Type);
        Assert.Equal(error, notification.Error);
        Assert.Same(targets, notification.NotificationTargets);
    }

    [Fact]
    public void Constructor_WithAllFields_SetsProperties()
    {
        // Arrange
        var id = "notif-1";
        var createdAt = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var updatedAt = new DateTime(2020, 1, 2, 0, 0, 0, DateTimeKind.Utc);
        var editId = "edit-2";
        var userId = "user-2";
        var userName = "User Two";
        var fileUrl = "http://file2.url";
        var type = NotificationType.Error;
        var error = "Some error";
        var targets = new List<NotificationTarget>();

        // Act
        var notification = new Notification(id, createdAt, updatedAt, editId, userId, userName, fileUrl, type, error, targets);

        // Assert
        Assert.Equal(id, notification.Id);
        Assert.Equal(createdAt, notification.CreatedAt);
        Assert.Equal(updatedAt, notification.UpdatedAt);
        Assert.Equal(editId, notification.EditId);
        Assert.Equal(userId, notification.UserId);
        Assert.Equal(userName, notification.UserName);
        Assert.Equal(fileUrl, notification.FileUrl);
        Assert.Equal(type, notification.Type);
        Assert.Equal(error, notification.Error);
        Assert.Same(targets, notification.NotificationTargets);
    }
}
