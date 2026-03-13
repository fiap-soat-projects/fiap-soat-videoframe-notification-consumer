using Adapter.Applications;
using Adapter.Gateways.Notifications.Resolvers.Interfaces;
using Adapter.Messages;
using Domain.Entities;
using Domain.Enums;
using Domain.Notifications.DTOs;
using Domain.Notifications.Interfaces;
using Domain.UseCases.Interfaces;
using Domain.ValueObjects;
using NSubstitute;

namespace UnitTests.Adapter.Applications.NotificationApplicationTests.Methods;

public class NotifyAsyncTests
{
    [Fact]
    public async Task When_NotifyAsyncCalled_Then_CreateNotification_and_SendNotifications_and_UpdateLogs()
    {
        // Arrange
        var notificationUseCase = Substitute.For<INotificationUseCase>();
        var notificationLogUseCase = Substitute.For<INotificationLogUseCase>();
        var resolver = Substitute.For<INotificationSenderResolver>();

        var targets = new List<NotificationTarget>
        {
            new() { Channel = NotificationChannel.Webhook, Target = "t1" },
            new() { Channel = NotificationChannel.Email, Target = "t2" }
        };

        var message = new NotificationMessage
        {
            EditId = "edit",
            UserId = "user",
            UserName = "name",
            FileUrl = "file",
            Type = NotificationType.Error,
            NotificationTargets = targets
        };

        var createdNotification = new Notification(
            "notification-id",
            DateTime.UtcNow,
            null,
            message.EditId,
            message.UserId,
            message.UserName,
            message.FileUrl,
            message.Type,
            message.Error,
            targets);

        notificationUseCase.CreateAsync(Arg.Any<Notification>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(createdNotification));

        // notification log create should return a log with an id
        notificationLogUseCase.CreateAsync(Arg.Any<NotificationLog>(), Arg.Any<CancellationToken>())
            .Returns(call =>
            {
                var log = call.ArgAt<NotificationLog>(0);
                return Task.FromResult(new NotificationLog("log-id", log.Target));
            });

        // resolver returns senders for channels
        var senderWebhook = Substitute.For<INotificationSender>();
        senderWebhook.Channel.Returns(NotificationChannel.Webhook);
        senderWebhook.NotifyAsync(Arg.Any<ChannelNotificationMessage>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new ChannelNotificationResult(NotificationStatus.Sent)));

        var senderEmail = Substitute.For<INotificationSender>();
        senderEmail.Channel.Returns(NotificationChannel.Email);
        senderEmail.NotifyAsync(Arg.Any<ChannelNotificationMessage>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new ChannelNotificationResult(NotificationStatus.Sent)));

        resolver.Resolve(NotificationChannel.Webhook).Returns(senderWebhook);
        resolver.Resolve(NotificationChannel.Email).Returns(senderEmail);

        var app = new NotificationApplication(notificationUseCase, notificationLogUseCase, resolver);

        // Act
        await app.NotifyAsync(message, CancellationToken.None);

        // Assert
        await notificationUseCase.Received(1).CreateAsync(Arg.Is<Notification>(n => n.EditId == message.EditId), Arg.Any<CancellationToken>());

        await notificationLogUseCase.Received(2).CreateAsync(Arg.Any<NotificationLog>(), Arg.Any<CancellationToken>());

        resolver.Received(1).Resolve(NotificationChannel.Webhook);
        resolver.Received(1).Resolve(NotificationChannel.Email);

        await notificationLogUseCase.Received(2).UpdateStatusAsync(Arg.Any<NotificationLog>(), Arg.Any<CancellationToken>());
    }
}
