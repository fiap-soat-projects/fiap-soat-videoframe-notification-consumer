using Domain.Enums;
using Domain.Notifications.DTOs;

namespace Domain.Notifications.Interfaces;

internal interface INotificationSender
{
    NotificationChannel Channel { get; }
    Task<ChannelNotificationResult> NotifyAsync(ChannelNotificationMessage message, CancellationToken cancellationToken);
}
