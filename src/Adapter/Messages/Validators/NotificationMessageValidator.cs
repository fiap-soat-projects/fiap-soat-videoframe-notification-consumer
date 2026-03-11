using Domain.Enums;

namespace Adapter.Messages.Validators;

internal static class NotificationMessageValidator
{
    internal static void Validate(NotificationMessage message)
    {
        ArgumentNullException.ThrowIfNull(message, nameof(message));
        ArgumentException.ThrowIfNullOrWhiteSpace(message.EditId, nameof(message.EditId));
        ArgumentException.ThrowIfNullOrWhiteSpace(message.UserId, nameof(message.UserId));
        ArgumentException.ThrowIfNullOrWhiteSpace(message.UserName, nameof(message.UserName));

        if (message.Type.Equals(NotificationType.Success))
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message.FileUrl, nameof(message.FileUrl));
        }

        if (message.NotificationTargets is null || message.NotificationTargets.Count is 0)
        {
            throw new InvalidOperationException("No notification target was defined");
        }

        var isTargetsInvalid = message
            .NotificationTargets
            .Select(notification => notification.Target)
            .Any(target => string.IsNullOrWhiteSpace(target));

        if (isTargetsInvalid)
        {
            throw new InvalidOperationException("Notifications must contains a valid target");
        }
    }
}
