using Domain.Enums;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Domain.Notifications.DTOs;

[ExcludeFromCodeCoverage]
public record ChannelNotificationMessage
{
    public required string EditId { get; init; }
    public required string UserName { get; init; }
    [JsonIgnore]
    public required string Target { get; init; }
    public required string FileUrl { get; init; }
    public required NotificationType Type { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InternalError { get; init; }
}
