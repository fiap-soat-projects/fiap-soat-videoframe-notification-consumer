using Domain.Entities.Enums;
using Domain.Notifications.DTOs;
using Domain.Notifications.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Gateways.Notifications.Senders;

[ExcludeFromCodeCoverage]
internal class WebhookSender : IWebhookSender
{
    public NotificationChannel Channel { get; private init; }

    public WebhookSender()
    {
        Channel = NotificationChannel.Webhook;
    }

    public Task<bool> SendAsync(WebhookMessage message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
