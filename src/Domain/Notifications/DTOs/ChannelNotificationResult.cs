using Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Notifications.DTOs;

[ExcludeFromCodeCoverage]
internal record ChannelNotificationResult
{
    public NotificationStatus Status { get; private init; }
    public string? InternalError { get; private init; }

    public ChannelNotificationResult(NotificationStatus status, string? internalError = null)
    {
        Status = status;
        InternalError = internalError;
    }
}
