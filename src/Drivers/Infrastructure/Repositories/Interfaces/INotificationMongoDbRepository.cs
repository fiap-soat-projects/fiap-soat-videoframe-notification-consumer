using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces;

internal interface INotificationMongoDbRepository
{
    Task CreateAsync(NotificationMongoDb notification, CancellationToken cancellationToken);
}
