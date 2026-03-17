using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin();

var myDatabase = postgres.AddDatabase("dbAspire");

var api = builder.AddProject<Api>("api")
    .WithReference(myDatabase)
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Aspire")
    .WaitFor(myDatabase);

builder.Build().Run();