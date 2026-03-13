using Domain.Enums;
using Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Messages;

[ExcludeFromCodeCoverage]
public record NotificationMessage
{
    public required string EditId { get; init; }
    public required string UserId { get; init; }
    public required string UserName { get; init; }
    public required string FileUrl { get; init; }
    public required NotificationType Type { get; init; }
    public string? Error { get; init; }
    public List<NotificationTarget> NotificationTargets { get; init; } = [];
}
