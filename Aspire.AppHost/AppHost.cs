using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume();

var myDatabase = postgres.AddDatabase("DefaultConnection");

var api = builder.AddProject<Api>("api")
    .WithReference(myDatabase)
    .WaitFor(myDatabase);

builder.Build().Run();