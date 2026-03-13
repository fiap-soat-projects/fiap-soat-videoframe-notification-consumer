using Adapter.Gateways.Notifications.Resolvers.Interfaces;
using Domain.Enums;
using Domain.Notifications.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Gateways.Notifications.Resolvers;

[ExcludeFromCodeCoverage]
internal class NotificationSenderResolver : INotificationSenderResolver
{
    private readonly IEnumerable<INotificationSender> _senders;

    public NotificationSenderResolver(IEnumerable<INotificationSender> senders)
    {
        _senders = senders;
    }

    public INotificationSender Resolve(NotificationChannel channel)
    {
        return _senders.FirstOrDefault(sender => sender.Channel == channel)
            ?? throw new InvalidOperationException($"No sender for {channel} was found");
    }
}
