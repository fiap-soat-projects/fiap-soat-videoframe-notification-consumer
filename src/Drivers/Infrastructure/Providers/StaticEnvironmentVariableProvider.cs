using Infrastructure.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Providers;

[ExcludeFromCodeCoverage]
internal static class StaticEnvironmentVariableProvider
{
    private const string KAFKA_HOST_ENV_VARIABLE_NAME = "KAFKA_HOST";
    private const string KAFKA_CONSUMER_GROUP_ENV_VARIABLE_NAME = "KAFKA_CONSUMER_GROUP";
    private const string NOTIFICATION_TOPIC_NAME_ENV_VARIABLE_NAME = "NOTIFICATION_TOPIC_NAME";
    private const string MONGODB_CONNECTION_STRING_ENV_VARIABLE_NAME = "MONGODB_CONNECTION_STRING";
    private const string APP_NAME_ENV_VARIABLE_NAME = "APP_NAME";

    internal static readonly string KafkaHost;
    internal static readonly string KafkaConsumerGroup;
    internal static readonly string NotificationTopicName;
    internal static readonly string MongoDbConnectionString;
    internal static readonly string AppName;

    static StaticEnvironmentVariableProvider()
    {
        KafkaHost = GetRequiredEnvironmentVariable(KAFKA_HOST_ENV_VARIABLE_NAME);
        KafkaConsumerGroup = GetRequiredEnvironmentVariable(KAFKA_CONSUMER_GROUP_ENV_VARIABLE_NAME);
        NotificationTopicName = GetRequiredEnvironmentVariable(NOTIFICATION_TOPIC_NAME_ENV_VARIABLE_NAME);
        MongoDbConnectionString = GetRequiredEnvironmentVariable(MONGODB_CONNECTION_STRING_ENV_VARIABLE_NAME);
        AppName = GetRequiredEnvironmentVariable(APP_NAME_ENV_VARIABLE_NAME);
    }

    internal static void Init() { }

    private static string GetRequiredEnvironmentVariable(string variableName)
    {
        var value = Environment.GetEnvironmentVariable(variableName);

        EnvironmentVariableNotFoundException.ThrowIfIsNullOrWhiteSpace(value, variableName);

        return value!;
    }
}
