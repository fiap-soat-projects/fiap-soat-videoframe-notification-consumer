using Domain.Entities.Enums;
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

    public Task<NotificationStatus> NotifyAsync(ChannelMessage message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
