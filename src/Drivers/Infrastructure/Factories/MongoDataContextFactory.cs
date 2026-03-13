using Infrastructure.Connections.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Contexts.Interfaces;
using Infrastructure.Providers;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Factories;

[ExcludeFromCodeCoverage]
internal class MongoDataContextFactory
{
    public static IMongoContext Create(IServiceProvider serviceProvider)
    {
        var mongoConnection = serviceProvider
            .GetServices<IMongoConnection>()
            .First(connection => connection.AppName == StaticEnvironmentVariableProvider.AppName);

        var mongoDatabase = mongoConnection
            .Client
            .GetDatabase(mongoConnection.MongoUrl.DatabaseName);

        return new MongoContext(StaticEnvironmentVariableProvider.AppName, mongoDatabase);
    }
}
