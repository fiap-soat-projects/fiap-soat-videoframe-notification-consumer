using Domain.Entities.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Notifications.DTOs;

[ExcludeFromCodeCoverage]
public record ChannelMessage
{
    public required string EditId { get; init; }
    public required string UserName { get; init; }
    public required string Target { get; init; }
    public required string FileUrl { get; init; }
    public required NotificationType Type { get; init; }
}
