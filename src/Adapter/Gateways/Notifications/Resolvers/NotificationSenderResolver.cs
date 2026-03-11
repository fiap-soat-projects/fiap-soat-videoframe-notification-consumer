using Adapter.Gateways.Notifications.Resolvers.Interfaces;
using Domain.Entities.Enums;
using Domain.Notifications.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Gateways.Notifications.Resolvers;

[ExcludeFromCodeCoverage]
internal class NotificationSenderResolver : INotificationSenderResolver
{
    
    public NotificationSenderResolver()
    {
        
    }

    public INotificationSender<TSender> Resolve<TSender>(NotificationChannel channel)
    {
        throw new NotImplementedException();
    }
}
