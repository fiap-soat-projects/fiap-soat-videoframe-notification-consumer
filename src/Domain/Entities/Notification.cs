using Domain.Enums;
using Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities;

internal class Notification
{
    public string? Id { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; private set; }
    public required string EditId { get; init; }
    public required string UserId { get; init; }
    public required string UserName { get; init; }
    public required string FileUrl { get; init; }
    public required NotificationType Type { get; init; }
    public string? Error { get; init; }
    public List<NotificationTarget> NotificationTargets { get; init; } = [];

    [SetsRequiredMembers]
    public Notification(
        string editId,
        string userId,
        string userName,
        string fileUrl,
        NotificationType type,
        string? error,
        List<NotificationTarget> notificationTargets)
    {
        CreatedAt = DateTime.UtcNow;
        EditId = editId;
        UserId = userId;
        UserName = userName;
        FileUrl = fileUrl;
        Type = type;
        Error = error;
        NotificationTargets = notificationTargets;
    }

    [SetsRequiredMembers]
    public Notification(
        string? id,
        DateTime createdAt,
        DateTime? updatedAt,
        string editId,
        string userId,
        string userName,
        string fileUrl,
        NotificationType type,
        string? error,
        List<NotificationTarget> notificationTargets)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        EditId = editId;
        UserId = userId;
        UserName = userName;
        FileUrl = fileUrl;
        Type = type;
        Error = error;
        NotificationTargets = notificationTargets;
    }

    internal void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
