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
    private const string EMAIL_SENDER_ENV_VARIABLE_NAME = "EMAIL_SENDER";
    private const string AWS_ACCESS_KEY_ID_ENV_VARIABLE_NAME = "AWS_ACCESS_KEY_ID";
    private const string AWS_SECRET_ACCESS_KEY_ENV_VARIABLE_NAME = "AWS_SECRET_ACCESS_KEY";
    private const string AWS_REGION_ENV_VARIABLE_NAME = "AWS_REGION";

    internal static readonly string KafkaHost;
    internal static readonly string KafkaConsumerGroup;
    internal static readonly string NotificationTopicName;
    internal static readonly string MongoDbConnectionString;
    internal static readonly string AppName;
    internal static readonly string EmailSender;
    internal static readonly string AwsAccessKeyId;
    internal static readonly string AwsSecretAccessKey;
    internal static readonly string AwsRegion;

    static StaticEnvironmentVariableProvider()
    {
        KafkaHost = GetRequiredEnvironmentVariable(KAFKA_HOST_ENV_VARIABLE_NAME);
        KafkaConsumerGroup = GetRequiredEnvironmentVariable(KAFKA_CONSUMER_GROUP_ENV_VARIABLE_NAME);
        NotificationTopicName = GetRequiredEnvironmentVariable(NOTIFICATION_TOPIC_NAME_ENV_VARIABLE_NAME);
        MongoDbConnectionString = GetRequiredEnvironmentVariable(MONGODB_CONNECTION_STRING_ENV_VARIABLE_NAME);
        AppName = GetRequiredEnvironmentVariable(APP_NAME_ENV_VARIABLE_NAME);
        EmailSender = GetRequiredEnvironmentVariable(EMAIL_SENDER_ENV_VARIABLE_NAME);
        AwsAccessKeyId = GetRequiredEnvironmentVariable(AWS_ACCESS_KEY_ID_ENV_VARIABLE_NAME);
        AwsSecretAccessKey = GetRequiredEnvironmentVariable(AWS_SECRET_ACCESS_KEY_ENV_VARIABLE_NAME);
        AwsRegion = GetRequiredEnvironmentVariable(AWS_REGION_ENV_VARIABLE_NAME);
    }

    internal static void Init() { }

    private static string GetRequiredEnvironmentVariable(string variableName)
    {
        var value = Environment.GetEnvironmentVariable(variableName);

        EnvironmentVariableNotFoundException.ThrowIfIsNullOrWhiteSpace(value, variableName);

        return value!;
    }
}
