var builder = WebApplication.CreateBuilder(args);

// Configure shared logging.
builder.ConfigureLogging();

// Add the services to the container.
builder.ConfigureServices();

var app = builder.Build();

// Configure the services.
app.Configure();

app.Run();
