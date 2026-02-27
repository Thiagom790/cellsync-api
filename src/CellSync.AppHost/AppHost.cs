var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("cellsyncDB")
    .WithDataVolume(isReadOnly: false);

var postgresDb = postgres.AddDatabase("cellsync");

builder.AddProject<Projects.CellSync_Api>("api")
    .WithHttpHealthCheck("/health")
    .WithReference(postgresDb)
    .WaitFor(postgresDb);

builder.Build().Run();