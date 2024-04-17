using BethanysPieShop.Shared.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configure shared logging.
builder.ConfigureLogging();

// Add services to the container.
builder.ConfigureServices();

var app = builder.Build();

// Set up the development tools, HTTPS redirection, authorization, and route mapping for controllers.
app.Configure();

app.Run();

public partial class Program { }
