using Domain.Entities.Enums;
using Domain.Notifications.DTOs;

namespace Domain.Notifications.Interfaces;

internal interface INotificationSender
{
    NotificationChannel Channel { get; }
    Task<NotificationStatus> NotifyAsync(ChannelMessage message, CancellationToken cancellationToken);
}
