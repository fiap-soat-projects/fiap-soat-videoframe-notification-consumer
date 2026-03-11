using Domain.ValueObjects;

namespace Domain.Entities;

internal class Notification
{
    public string? Id { get; set; }
    public required string EditId { get; init; }
    public required string UserId { get; init; }
    public required string UserName { get; init; }
    public List<NotificationTarget> NotificationTargets { get; init; } = [];
    public required string FileUrl { get; init; }
}
