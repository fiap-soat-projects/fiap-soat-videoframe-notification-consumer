using Adapter.Applications.DTOs;
using Adapter.Applications.Interfaces;
using Adapter.Gateways.Notifications.Resolvers.Interfaces;
using Domain.Notifications.DTOs;
using Domain.UseCases.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Adapter.Applications;

internal class NotificationApplication : INotificationApplication
{
    private readonly INotificationUseCase _notificationUseCase;
    private readonly INotificationLogUseCase _notificationLogUseCase;
    private readonly INotificationSenderResolver _notificationSenderResolver;

    public NotificationApplication(
        INotificationUseCase notificationUseCase,
        INotificationLogUseCase notificationLogUseCase,
        INotificationSenderResolver notificationSenderResolver)
    {
        _notificationUseCase = notificationUseCase;
        _notificationLogUseCase = notificationLogUseCase;
        _notificationSenderResolver = notificationSenderResolver;
    }

    public Task NotifyAsync(NotificationMessage message, CancellationToken cancellationToken)
    {
        // Cria Notification
        // Salva notification no banco
        // Para cada target:
        // Salva no banco como Pending
        // Envia notificação
        // Atualiza status da notificação no banco


        var a = _notificationSenderResolver.Resolve(Domain.Entities.Enums.NotificationChannel.Email);

        var whMessage = new WebhookMessage();

        a.SendAsync(whMessage, cancellationToken);

        throw new NotImplementedException();
    }
}
