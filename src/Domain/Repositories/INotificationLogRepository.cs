using Domain.Entities;

namespace Domain.Repositories;

internal interface INotificationLogRepository
{
    Task<NotificationLog> CreateAsync(NotificationLog log, CancellationToken cancellationToken);
    Task UpdateStatusAsync(NotificationLog log, CancellationToken cancellationToken);
}
