using Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Applications.DTOs;

[ExcludeFromCodeCoverage]
public record NotificationMessage
{
    public required string EditId { get; init; }
    public required string UserId { get; init; }
    public required string UserName { get; init; }
    public List<NotificationTarget> NotificationTargets { get; init; } = [];
    public required string FileUrl { get; init; }
}
