using Adapter.Gateways.Notifications.Senders.DTOs;
using Domain.Enums;
using Domain.Notifications.DTOs;
using Domain.Notifications.Interfaces;
using Infrastructure.Providers;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;

namespace Adapter.Gateways.Notifications.Senders;

[ExcludeFromCodeCoverage]
internal class WebhookSender : IWebhookSender
{
    public NotificationChannel Channel => NotificationChannel.Webhook;

    private readonly HttpClient _client;
    private readonly ILogger<WebhookSender> _logger;

    public WebhookSender(HttpClient client, ILogger<WebhookSender> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<ChannelNotificationResult> NotifyAsync(ChannelNotificationMessage message, CancellationToken cancellationToken)
    {
        var payload = JsonSerializer.Serialize(new WebhookPayload(message), JsonSerializerOptionsProvider.SerializerOptions);

        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(message.Target, content, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Email notification sent successfully to {Target} for edit {EditId}", message.Target, message.EditId);

            return new ChannelNotificationResult(NotificationStatus.Sent);
        }

        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        _logger.LogError("Failed to send email notification to {Target} for edit {EditId}. " +
            "Status Code: {StatusCode}, " +
            "Response: {ResponseBody}",
            message.Target,
            message.EditId,
            response.StatusCode,
            responseBody);

        return new ChannelNotificationResult(NotificationStatus.Failed, responseBody);
    }
}
