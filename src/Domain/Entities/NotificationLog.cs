using Domain.Enums;
using Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities;

internal class NotificationLog
{
    public string? Id { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; private set; }
    public required string NotificationId { get; init; }
    public required NotificationTarget Target { get; init; }
    public NotificationStatus Status { get; private set; }
    public string? Error { get; private set; }

    [SetsRequiredMembers]
    public NotificationLog(string notificationId, NotificationTarget target)
    {
        CreatedAt = DateTime.UtcNow;
        NotificationId = notificationId;
        Target = target;
        Status = NotificationStatus.Pending;
    }

    [SetsRequiredMembers]
    public NotificationLog(
        string? id,
        DateTime createdAt,
        DateTime? updatedAt,
        string notificationId,
        NotificationTarget target,
        NotificationStatus status,
        string? error)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        NotificationId = notificationId;
        Target = target;
        Status = status;
        Error = error;
    }

    internal void SetSendStatus(NotificationStatus status, string? error = null)
    {
        UpdatedAt = DateTime.UtcNow;
        Status = status;
        Error = error;
    }
}
