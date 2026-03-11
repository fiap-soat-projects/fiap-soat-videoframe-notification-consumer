using Adapter.Gateways.Notifications.Senders;
using Domain.Notifications.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Adapter;

[ExcludeFromCodeCoverage]
public static class AdapterExtensions
{
    public static IServiceCollection AddAdapter(this IServiceCollection services)
    {
        services
            .AddSingleton<IWebhookSender, WebhookSender>()
            .AddSingleton<IEmailSender, EmailSender>();

        return services;
    }
}
