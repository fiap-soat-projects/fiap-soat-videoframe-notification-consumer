using Domain.Entities;

namespace Domain.UseCases.Interfaces;

internal interface INotificationLogUseCase
{
    Task<NotificationLog> CreateAsync(NotificationLog log, CancellationToken cancellationToken);
    Task UpdateStatusAsync(NotificationLog log, CancellationToken cancellationToken);
}
