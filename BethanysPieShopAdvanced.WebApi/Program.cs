var builder = WebApplication.CreateBuilder(args);

// Configure shared logging.
builder.ConfigureLogging();

// Add services to the container via shared configuration (includes DbContext, repos, services, mapper).
builder.AddServices();
builder.ConfigureServices();

var app = builder.Build();

// Set up the development tools, HTTPS redirection, authorization, and route mapping for controllers.
app.Configure();

app.Run();

public partial class Program { }
