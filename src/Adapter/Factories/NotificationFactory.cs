using Adapter.Messages;
using Domain.Entities;

namespace Adapter.Factories;

internal static class NotificationFactory
{
    internal static Notification Create(NotificationMessage message)
    {
        return new Notification(
            message.EditId,
            message.UserId,
            message.UserName,
            message.FileUrl,
            message.Type,
            message.Error,
            message.NotificationTargets
        );
    }
}
