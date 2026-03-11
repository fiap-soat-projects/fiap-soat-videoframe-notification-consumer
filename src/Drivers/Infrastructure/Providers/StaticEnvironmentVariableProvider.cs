using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Providers;

[ExcludeFromCodeCoverage]
internal static class StaticEnvironmentVariableProvider
{
    private const string KAFKA_HOST_ENV_VARIABLE_NAME = "KAFKA_HOST";
    private const string KAFKA_CONSUMER_GROUP_ENV_VARIABLE_NAME = "KAFKA_CONSUMER_GROUP";

    internal static readonly string KafkaHost;
    internal static readonly string KafkaConsumerGroup;

    static StaticEnvironmentVariableProvider()
    {
        KafkaHost = GetRequiredEnvironmentVariable(KAFKA_HOST_ENV_VARIABLE_NAME);
        KafkaConsumerGroup = GetRequiredEnvironmentVariable(KAFKA_CONSUMER_GROUP_ENV_VARIABLE_NAME);
    }

    internal static void Init() { }

    private static string GetRequiredEnvironmentVariable(string variableName)
    {
        var value = Environment.GetEnvironmentVariable(variableName);

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Environment variable '{variableName}' is not set.");
        }

        return value;
    }
}
