using System.Diagnostics.CodeAnalysis;

namespace Domain.Notifications.DTOs;

[ExcludeFromCodeCoverage]
public record EmailMessage : ChannelMessage
{
    public required string Subject { get; init; }
    public required string Target { get; init; }
    public required string Body { get; init; }
}
