using Domain.Entities.Enums;
using Domain.Notifications.DTOs;
using Domain.Notifications.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Gateways.Notifications.Senders;

[ExcludeFromCodeCoverage]
internal class EmailSender : IEmailSender
{
    public NotificationChannel Channel { get; private init; }

    public EmailSender()
    {
        Channel = NotificationChannel.Email;
    }

    public Task<bool> SendAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
