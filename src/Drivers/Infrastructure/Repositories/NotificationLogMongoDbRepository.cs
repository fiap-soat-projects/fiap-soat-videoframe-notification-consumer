using Infrastructure.Contexts.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
internal class NotificationLogMongoDbRepository : INotificationLogMongoDbRepository
{
    private readonly IMongoCollection<NotificationLogMongoDb> _mongoCollection;

    public NotificationLogMongoDbRepository(IMongoContext mongoContext)
    {
        _mongoCollection = mongoContext.GetCollection<NotificationLogMongoDb>();
    }

    public async Task CreateAsync(NotificationLogMongoDb log, CancellationToken cancellationToken)
    {
        await _mongoCollection.InsertOneAsync(log, cancellationToken: cancellationToken);
    }

    public async Task UpdateStatusAsync(NotificationLogMongoDb log, CancellationToken cancellationToken)
    {
        var update = Builders<NotificationLogMongoDb>.Update
            .Set(x => x.UpdatedAt, log.UpdatedAt)
            .Set(x => x.Status, log.Status);

        if (string.IsNullOrWhiteSpace(log.Error) is false)
        {
            update.Set(x => x.Error, log.Error);
        }

        await _mongoCollection.UpdateOneAsync(
            Builders<NotificationLogMongoDb>.Filter.Eq(x => x.Id, log.Id),
            update,
            cancellationToken: cancellationToken);
    }
}
