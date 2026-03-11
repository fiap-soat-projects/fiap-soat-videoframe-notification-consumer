using Adapter.Applications.DTOs;

namespace Adapter.Applications.Interfaces;

public interface INotificationApplication
{
    Task NotifyAsync(NotificationMessage message, CancellationToken cancellationToken);
}
