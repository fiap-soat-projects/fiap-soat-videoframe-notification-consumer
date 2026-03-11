using Adapter.Applications;
using Adapter.Applications.Interfaces;
using Adapter.Gateways.Notifications.Resolvers;
using Adapter.Gateways.Notifications.Resolvers.Interfaces;
using Adapter.Gateways.Notifications.Senders;
using Adapter.Gateways.Repositories;
using Domain.Notifications.Interfaces;
using Domain.Repositories;
using Domain.UseCases;
using Domain.UseCases.Interfaces;
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
            .AddSingleton<IEmailSender, EmailSender>()
            .AddSingleton<INotificationSenderResolver, NotificationSenderResolver>();

        services
            .AddSingleton<INotificationRepository, NotificationRepository>()
            .AddSingleton<INotificationLogRepository, NotificationLogRepository>();

        services
            .AddSingleton<INotificationUseCase, NotificationUseCase>()
            .AddSingleton<INotificationLogUseCase, NotificationLogUseCase>();

        services.AddSingleton<INotificationApplication, NotificationApplication>();

        return services;
    }
}
