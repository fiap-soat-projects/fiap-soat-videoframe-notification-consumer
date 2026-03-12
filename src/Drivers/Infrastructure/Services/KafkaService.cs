using Confluent.Kafka;
using Infrastructure.Providers;
using Infrastructure.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Infrastructure.Services;

[ExcludeFromCodeCoverage]
internal class KafkaService : IKafkaService
{
    private readonly IConsumer<Ignore, string> _consumer;

    public KafkaService()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = StaticEnvironmentVariableProvider.KafkaHost,
            GroupId = StaticEnvironmentVariableProvider.KafkaConsumerGroup,
            SecurityProtocol = SecurityProtocol.Plaintext,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
    }

    public void Subscribe(string topic)
    {
        _consumer.Subscribe(topic);
    }

    public TClass Consume<TClass>(CancellationToken cancellationToken)
    {
        var consumeResult = _consumer.Consume(cancellationToken);

        ArgumentException.ThrowIfNullOrWhiteSpace(consumeResult?.Message.Value);

        var message = JsonSerializer.Deserialize<TClass>(
            consumeResult.Message.Value,
            JsonSerializerOptionsProvider.SerializerOptions);

        return message!;
    }

    public void Commit()
    {
        _consumer.Commit();
    }

    public void Dispose()
    {
        _consumer.Close();
    }
}
