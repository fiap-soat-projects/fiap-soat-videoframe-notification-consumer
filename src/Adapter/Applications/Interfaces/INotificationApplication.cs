using Adapter.Messages;

namespace Adapter.Applications.Interfaces;

public interface INotificationApplication
{
    Task NotifyAsync(NotificationMessage message, CancellationToken cancellationToken);
}
