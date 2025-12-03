var builder = WebApplication.CreateBuilder(args);

// Use SqlServer only in normal runs; tests will replace this service
builder.Services.AddDbContext<PieShopDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connStr);
});

// Configure shared logging.
builder.ConfigureLogging();

// Add services to the container.
builder.ConfigureServices();

var app = builder.Build();

// Set up the development tools, HTTPS redirection, authorization, and route mapping for controllers.
app.Configure();

app.Run();

// Todo: Remove it if not needed for tests.
public partial class Program { }
