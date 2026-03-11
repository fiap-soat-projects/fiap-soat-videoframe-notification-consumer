using Domain.Entities;

namespace Domain.Repositories;

internal interface INotificationLogRepository
{
    Task<NotificationLog> CreateAsync(NotificationLog log, CancellationToken cancellationToken);
}
