using Domain.Entities;
using Domain.Repositories;

namespace Adapter.Gateways.Repositories;

internal class NotificationRepository : INotificationRepository
{
    public Task<Notification> CreateAsync(Notification notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
