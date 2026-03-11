using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Gateways.Repositories;

[ExcludeFromCodeCoverage]
internal class NotificationLogRepository : INotificationLogRepository
{
    private readonly INotificationLogMongoDbRepository _mongoDbRepository;

    public NotificationLogRepository(INotificationLogMongoDbRepository mongoDbRepository)
    {
        _mongoDbRepository = mongoDbRepository;
    }

    public async Task<NotificationLog> CreateAsync(NotificationLog log, CancellationToken cancellationToken)
    {
        var logMongoDb = new NotificationLogMongoDb(log);

        await _mongoDbRepository.CreateAsync(logMongoDb, cancellationToken);

        return logMongoDb.ToDomain();
    }

    public async Task UpdateStatusAsync(NotificationLog log, CancellationToken cancellationToken)
    {
        var logMongoDb = new NotificationLogMongoDb(log);

        await _mongoDbRepository.UpdateStatusAsync(logMongoDb, cancellationToken);
    }
}
