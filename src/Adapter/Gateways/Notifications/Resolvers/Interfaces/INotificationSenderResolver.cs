using Domain.Entities.Enums;
using Domain.Notifications.Interfaces;

namespace Adapter.Gateways.Notifications.Resolvers.Interfaces;

internal interface INotificationSenderResolver
{
    INotificationSender Resolve(NotificationChannel channel);
}
