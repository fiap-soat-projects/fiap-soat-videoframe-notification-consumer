using Domain.Entities.Enums;
using Domain.Notifications.DTOs;
using Domain.Notifications.Interfaces;

namespace Adapter.Gateways.Notifications.Resolvers.Interfaces;

internal interface INotificationSenderResolver
{
    INotificationSender<ChannelMessage> Resolve(NotificationChannel channel);
}
