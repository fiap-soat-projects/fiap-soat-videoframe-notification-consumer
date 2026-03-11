using Domain.Entities.Enums;

namespace Domain.ValueObjects;

public record NotificationTarget
{
    public required NotificationChannel Channel { get; init; }
    public required NotificationType Type { get; init; }
    public required string Target { get; init; }
}
