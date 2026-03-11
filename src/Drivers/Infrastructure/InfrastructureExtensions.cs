using Infrastructure.Connections;
using Infrastructure.Connections.Interfaces;
using Infrastructure.Factories;
using Infrastructure.Options;
using Infrastructure.Providers;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure;

[ExcludeFromCodeCoverage]
public static class InfrastructureExtensions
{
    const string DEFAULT_CLUSTER_NAME = "default";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        StaticEnvironmentVariableProvider.Init();

        MongoGlobalOptions.Init();

        var mongoConnectionString = StaticEnvironmentVariableProvider.MongoDbConnectionString;
        var connection = new MongoConnection(
            DEFAULT_CLUSTER_NAME,
            mongoConnectionString!,
            StaticEnvironmentVariableProvider.AppName);

        services
            .AddSingleton<IMongoConnection>(connection)
            .AddSingleton(MongoDataContextFactory.Create);

        services
            .AddSingleton<INotificationMongoDbRepository, NotificationMongoDbRepository>()
            .AddSingleton<INotificationLogMongoDbRepository, NotificationLogMongoDbRepository>();

        services.AddHttpClient();

        services.AddSingleton<IKafkaService, KafkaService>();

        return services;
    }
}
