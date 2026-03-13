using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Entities;

[ExcludeFromCodeCoverage]
[BsonIgnoreExtraElements]
[BsonDiscriminator("notification")]
internal class NotificationMongoDb
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string EditId { get; set; }
    public required string UserId { get; set; }
    public required string UserName { get; set; }
    public required string FileUrl { get; set; }
    public required NotificationType Type { get; set; }
    [BsonIgnoreIfNull]
    public string? Error { get; set; }
    public List<NotificationTarget> NotificationTargets { get; set; } = [];

    [SetsRequiredMembers]
    public NotificationMongoDb(Notification notification)
    {
        Id = notification.Id;
        CreatedAt = notification.CreatedAt;
        UpdatedAt = notification.UpdatedAt;
        EditId = notification.EditId;
        UserId = notification.UserId;
        UserName = notification.UserName;
        FileUrl = notification.FileUrl;
        Type = notification.Type;
        Error = notification.Error;
        NotificationTargets = notification.NotificationTargets;
    }

    public Notification ToDomain()
    {
        return new Notification(
            Id,
            CreatedAt,
            UpdatedAt,
            EditId,
            UserId,
            UserName,
            FileUrl,
            Type,
            Error,
            NotificationTargets
        );
    }
}
