using Domain.Entities.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

internal class NotificationLog
{
    public string? Id { get; set; }
    public required string NotificationId { get; set; }
    public required NotificationTarget Target { get; init; }
    public NotificationStatus Status { get; set; }
}
