using Domain.Notifications.DTOs;

namespace Domain.Notifications.Interfaces;

internal interface IWebhookSender : INotificationSender<WebhookMessage>
{

}
