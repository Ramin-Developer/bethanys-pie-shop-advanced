var builder = WebApplication.CreateBuilder(args);

// Configure shared logging.
builder.ConfigureLogging();

// Add services from the shared configuration (DbContext, repos, services, mapper).
builder.AddServices();
builder.ConfigureServices();

var app = builder.Build();

// Configure the services.
app.Configure();

app.Run();
