using Infrastructure.Contexts.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
internal class NotificationMongoDbRepository : INotificationMongoDbRepository
{
    private readonly IMongoCollection<NotificationMongoDb> _mongoCollection;

    public NotificationMongoDbRepository(IMongoContext mongoContext)
    {
        _mongoCollection = mongoContext.GetCollection<NotificationMongoDb>();
    }

    public async Task CreateAsync(NotificationMongoDb notification, CancellationToken cancellationToken)
    {
        await _mongoCollection.InsertOneAsync(notification, cancellationToken: cancellationToken);
    }
}
