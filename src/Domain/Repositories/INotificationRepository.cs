using Domain.Entities;

namespace Domain.Repositories;

internal interface INotificationRepository
{
    Task<Notification> CreateAsync(Notification notification, CancellationToken cancellationToken);
}
