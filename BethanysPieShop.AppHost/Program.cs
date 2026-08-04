var builder = DistributedApplication.CreateBuilder(args);

// Resolve the SQL connection string from configuration (appsettings, user secrets, or environment).
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' is not configured. " +
        "Set it in appsettings.Development.json, user secrets, or an environment variable.");

// Add the Admin (MVC) project as an orchestrated resource.
var admin = builder.AddProject<Projects.BethanysPieShop_Admin>("admin")
    .WithEnvironment("ConnectionStrings__DefaultConnection", connectionString);

// Add the Web API project as an orchestrated resource.
var api = builder.AddProject<Projects.BethanysPieShop_WebApi>("webapi")
    .WithEnvironment("ConnectionStrings__DefaultConnection", connectionString);

// Express the dependency: the Admin UI references the Web API.
admin.WithReference(api);

builder.Build().Run();