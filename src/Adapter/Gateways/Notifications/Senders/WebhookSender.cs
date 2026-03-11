using Domain.Enums;
using Domain.Notifications.DTOs;
using Domain.Notifications.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Gateways.Notifications.Senders;

[ExcludeFromCodeCoverage]
internal class WebhookSender : IWebhookSender
{
    public NotificationChannel Channel => NotificationChannel.Webhook;

    public WebhookSender()
    {

    }

    public Task<ChannelNotificationResult> NotifyAsync(ChannelNotificationMessage message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
