using Domain.Entities;

namespace Domain.UseCases.Interfaces;

internal interface INotificationUseCase
{
    Task<Notification> CreateAsync(Notification notification, CancellationToken cancellationToken);
}
