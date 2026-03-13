using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces;

internal interface INotificationLogMongoDbRepository
{
    Task CreateAsync(NotificationLogMongoDb log, CancellationToken cancellationToken);
    Task UpdateStatusAsync(NotificationLogMongoDb log, CancellationToken cancellationToken);
}
