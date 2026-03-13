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

        await HandleNotificationsAsync(message, notification, cancellationToken);
    }

    private async Task HandleNotificationsAsync(        
        NotificationMessage message,
        Notification notification,
        CancellationToken cancellationToken)
    {
        foreach (var target in notification.NotificationTargets)
        {
            var notificationLog = await _notificationLogUseCase.CreateAsync(
                new NotificationLog(notification.Id!, target),
                cancellationToken);

            var channelMessage = new ChannelNotificationMessage
            {
                EditId = message.EditId,
                UserName = message.UserName,
                FileUrl = message.FileUrl,
                Target = target.Target,
                Type = message.Type,
                InternalError = message.Error
            };

            var notificationStatus = await _notificationSenderResolver
                .Resolve(target.Channel)
                .NotifyAsync(channelMessage, cancellationToken);

            notificationLog.SetSendStatus(notificationStatus.Status, notificationStatus.InternalError);

            await _notificationLogUseCase.UpdateStatusAsync(notificationLog, cancellationToken);
        }
    }
}
