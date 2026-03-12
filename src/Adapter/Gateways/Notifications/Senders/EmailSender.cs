using Adapter.Gateways.Notifications.Constants;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Domain.Enums;
using Domain.Notifications.DTOs;
using Domain.Notifications.Interfaces;
using Infrastructure.Providers;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Gateways.Notifications.Senders;

[ExcludeFromCodeCoverage]
internal class EmailSender : IEmailSender
{
    public NotificationChannel Channel => NotificationChannel.Email;

    private readonly IAmazonSimpleEmailService _client;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(IAmazonSimpleEmailService client, ILogger<EmailSender> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<ChannelNotificationResult> NotifyAsync(ChannelNotificationMessage message, CancellationToken cancellationToken)
    {
        SendEmailResponse? response;

        var request = BuildEmailRequest(message);

        try
        {
            response = await _client.SendEmailAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email notification for EditId: {EditId}", message.EditId);

            return new ChannelNotificationResult(NotificationStatus.Failed, ex.Message);
        }

        _logger.LogInformation("Email sent successfully: {EmailId}", response?.MessageId);

        return new ChannelNotificationResult(NotificationStatus.Sent);
    }

    private static SendEmailRequest BuildEmailRequest(ChannelNotificationMessage message)
    {
        string subject;
        string body;
        var isSuccessMessage = message.Type.Equals(Domain.Enums.NotificationType.Success);

        if (isSuccessMessage)
        {
            subject = EmailNotificationConstants.EMAIL_SUBJECT_FOR_SUCCESSFUL_VIDEO_PROCESSING;
            body = GenerateSuccessEmail(message.EditId, message.UserName, message.FileUrl);
        }
        else
        {
            subject = EmailNotificationConstants.EMAIL_SUBJECT_FOR_ERROR_VIDEO_PROCESSING;
            body = GenerateErrorEmail(message.EditId, message.UserName, message.InternalError!);
        }

        return new SendEmailRequest
        {
            Source = StaticEnvironmentVariableProvider.EmailSender,
            Destination = new Destination { ToAddresses = [message.Target] },
            Message = new Message
            {
                Subject = new Content(subject),
                Body = new Body { Text = new Content(body) }
            }
        };
    }

    public static string GenerateSuccessEmail(string editId, string userName, string downloadUrl)
    {
        var template = LoadTemplate(EmailNotificationConstants.EMAIL_TEMPLATE_FOR_SUCCESSFUL_VIDEO_PROCESSING);

        return FillTemplate(template, new Dictionary<string, string>
        {
            { "EditId", editId },
            { "UserName", userName },
            { "DownloadUrl", downloadUrl }
        });
    }

    public static string GenerateErrorEmail(string editId, string userName, string error)
    {
        var template = LoadTemplate(EmailNotificationConstants.EMAIL_TEMPLATE_FOR_ERROR_VIDEO_PROCESSING);

        return FillTemplate(template, new Dictionary<string, string>
        {
            { "EditId", editId },
            { "UserName", userName },
            { "Error", error }
        });
    }

    private static string LoadTemplate(string templateName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Templates", templateName);

        return File.ReadAllText(path);
    }

    private static string FillTemplate(string template, Dictionary<string, string> values)
    {
        foreach (var item in values)
        {
            template = template.Replace($"{{{{{item.Key}}}}}", item.Value);
        }

        return template;
    }
}
