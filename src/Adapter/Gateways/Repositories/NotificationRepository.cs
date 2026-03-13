using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Gateways.Repositories;

[ExcludeFromCodeCoverage]
internal class NotificationRepository : INotificationRepository
{
    private readonly INotificationMongoDbRepository _mongoDbRepository;

    public NotificationRepository(INotificationMongoDbRepository mongoDbRepository)
    {
        _mongoDbRepository = mongoDbRepository;
    }

    public async Task<Notification> CreateAsync(Notification notification, CancellationToken cancellationToken)
    {
        var notificationMongoDb = new NotificationMongoDb(notification);

        await _mongoDbRepository.CreateAsync(notificationMongoDb, cancellationToken);

        return notificationMongoDb.ToDomain();
    }
}
