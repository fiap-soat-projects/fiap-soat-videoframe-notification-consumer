using Domain.Notifications.DTOs;

namespace Domain.Notifications.Interfaces;

internal interface IEmailSender : INotificationSender<EmailMessage>
{

}
