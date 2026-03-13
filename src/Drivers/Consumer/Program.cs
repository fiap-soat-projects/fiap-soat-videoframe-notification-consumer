using Adapter;
using Consumer;
using Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddInfrastructure()
    .AddAdapter()
    .AddHostedService<Worker>();

var host = builder.Build();
host.Run();
