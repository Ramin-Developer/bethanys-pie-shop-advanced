// Todo: Register a IWebApiClient interface and implement it in WebApiClient. This should abstract away Http calls.
// Todo: Use it in this project instead of BusinessLogic for CRUD operations.

using BethanysPieShop.Shared.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configure shared logging.
builder.ConfigureLogging();

// Add the services to the container.
builder.ConfigureServices();

var app = builder.Build();

// Configure the services.
app.Configure();

app.Run();
