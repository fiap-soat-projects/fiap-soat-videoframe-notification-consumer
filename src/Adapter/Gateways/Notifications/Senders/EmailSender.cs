using Domain.Enums;
using Domain.Notifications.DTOs;
using Domain.Notifications.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Gateways.Notifications.Senders;

[ExcludeFromCodeCoverage]
internal class EmailSender : IEmailSender
{
    public NotificationChannel Channel => NotificationChannel.Email;

    public EmailSender()
    {

    }

    public Task<ChannelNotificationResult> NotifyAsync(ChannelNotificationMessage message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
