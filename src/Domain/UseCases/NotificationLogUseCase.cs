using Domain.Entities;
using Domain.Repositories;
using Domain.UseCases.Interfaces;

namespace Domain.UseCases;

internal class NotificationLogUseCase : INotificationLogUseCase
{
    private readonly INotificationLogRepository _repository;

    public NotificationLogUseCase(INotificationLogRepository repository)
    {
        _repository = repository;
    }

    public async Task<NotificationLog> CreateAsync(NotificationLog log, CancellationToken cancellationToken)
    {
        log = await _repository.CreateAsync(log, cancellationToken);

        return log;
    }

    public async Task UpdateStatusAsync(NotificationLog log, CancellationToken cancellationToken)
    {
        await _repository.UpdateStatusAsync(log, cancellationToken);
    }
}
