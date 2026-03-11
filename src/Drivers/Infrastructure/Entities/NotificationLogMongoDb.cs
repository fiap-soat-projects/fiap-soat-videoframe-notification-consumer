using Domain.Enums;
using Domain.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Entities;

[ExcludeFromCodeCoverage]
[BsonIgnoreExtraElements]
[BsonDiscriminator("notificationLog")]
internal class NotificationLogMongoDb
{
    public string? Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string NotificationId { get; set; }
    public required NotificationTarget Target { get; set; }
    public NotificationStatus Status { get; set; }
    public string? Error { get; set; }
}
