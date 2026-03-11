namespace Infrastructure.Services.Interfaces;

public interface IKafkaService : IDisposable
{
    void Subscribe(string topic);
    TClass Consume<TClass>(CancellationToken cancellationToken);
    void Commit();
}
