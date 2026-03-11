using Adapter.Applications.Interfaces;
using Adapter.Factories;
using Adapter.Gateways.Notifications.Resolvers.Interfaces;
using Adapter.Messages;
using Adapter.Messages.Validators;
using Domain.Entities;
using Domain.Notifications.DTOs;
using Domain.UseCases.Interfaces;
using Domain.ValueObjects;

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

    public async Task NotifyAsync(NotificationMessage message, CancellationToken cancellationToken)
    {
        NotificationMessageValidator.Validate(message);

        var notification = await _notificationUseCase.CreateAsync(NotificationFactory.Create(message), cancellationToken);

        await HandleNotificationsAsync(notification.Id!, message, notification.NotificationTargets, cancellationToken);
    }

    private async Task HandleNotificationsAsync(
        string notificationId,
        NotificationMessage message,
        List<NotificationTarget> notifications,
        CancellationToken cancellationToken)
    {
        foreach (var notification in notifications)
        {
            var notificationLog = await _notificationLogUseCase.CreateAsync(
                new NotificationLog(notificationId, notification),
                cancellationToken);

            var channelMessage = new ChannelNotificationMessage
            {
                EditId = message.EditId,
                UserName = message.UserName,
                FileUrl = message.FileUrl,
                Target = notification.Target,
                Type = message.Type,
            };

            var notificationStatus = await _notificationSenderResolver
                .Resolve(notification.Channel)
                .NotifyAsync(channelMessage, cancellationToken);

            notificationLog.SetSendStatus(notificationStatus.Status, notificationStatus.InternalError);

            await _notificationLogUseCase.UpdateStatusAsync(notificationLog, cancellationToken);
        }
    }
}
