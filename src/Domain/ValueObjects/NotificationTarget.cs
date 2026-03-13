using Domain.Enums;

namespace Domain.ValueObjects;

public record NotificationTarget
{
    public required NotificationChannel Channel { get; init; }
    public required string Target { get; init; }
}
