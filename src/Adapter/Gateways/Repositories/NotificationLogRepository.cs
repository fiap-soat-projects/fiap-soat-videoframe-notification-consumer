using Domain.Entities;
using Domain.Repositories;

namespace Adapter.Gateways.Repositories;

internal class NotificationLogRepository : INotificationLogRepository
{
    public Task<NotificationLog> CreateAsync(NotificationLog log, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateStatusAsync(NotificationLog log, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
