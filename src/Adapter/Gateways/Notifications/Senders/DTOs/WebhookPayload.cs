using Domain.Enums;
using Domain.Notifications.DTOs;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Adapter.Gateways.Notifications.Senders.DTOs;

[ExcludeFromCodeCoverage]
internal record WebhookPayload
{
    public string? EditId { get; init; }
    public string? UserName { get; init; }
    public string? FileUrl { get; init; }
    public NotificationType Type { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InternalError { get; init; }

    public WebhookPayload(ChannelNotificationMessage message)
    {
        EditId = message.EditId;
        UserName = message.UserName;
        FileUrl = message.FileUrl;
        Type = message.Type;
        InternalError = message.InternalError;
    }
}
