using Domain.Entities.Enums;
using Domain.Notifications.DTOs;

namespace Domain.Notifications.Interfaces;

internal interface INotificationSender<TMessage> where TMessage : ChannelMessage
{
    NotificationChannel Channel { get; }
    Task<bool> SendAsync(TMessage message, CancellationToken cancellationToken);
}
