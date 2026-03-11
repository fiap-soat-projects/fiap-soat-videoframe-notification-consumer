using Domain.Entities;

namespace Domain.UseCases.Interfaces;

internal interface INotificationLogUseCase
{
    Task<NotificationLog> CreateAsync(NotificationLog notification, CancellationToken cancellationToken);
    Task UpdateStatusAsync(NotificationLog log, CancellationToken cancellationToken);
}
