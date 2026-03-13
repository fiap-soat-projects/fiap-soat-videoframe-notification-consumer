using Domain.Entities;
using Domain.Repositories;
using Domain.UseCases.Interfaces;

namespace Domain.UseCases;

internal class NotificationUseCase : INotificationUseCase
{
    private readonly INotificationRepository _repository;

    public NotificationUseCase(INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Notification> CreateAsync(Notification notification, CancellationToken cancellationToken)
    {
        notification = await _repository.CreateAsync(notification, cancellationToken);

        return notification;
    }
}
