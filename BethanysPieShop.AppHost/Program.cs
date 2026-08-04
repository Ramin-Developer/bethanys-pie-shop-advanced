//using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var localConn =
    "Server=localhost;Database=pieshopdb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True";

if (string.IsNullOrWhiteSpace(localConn))
{
    throw new ArgumentException("The SQL connection string 'localConn' is empty.", nameof(localConn));
}

// Locate the Admin project relative to the app host
var repoRoot = FindRoot(AppContext.BaseDirectory, "BethanysPieShopAdvanced.Admin");
var adminProject = Path.Combine(repoRoot, "BethanysPieShopAdvanced.Admin", "BethanysPieShop.Admin.csproj");

if (!File.Exists(adminProject))
{
    throw new FileNotFoundException($"Admin project not found at '{adminProject}'.", adminProject)
    {
        HResult = 404,
        Source = "BethanysPieShop.AppHost"
    };
}

// Locate the WebApi project similarly
var apiProject = Path.Combine(repoRoot, "BethanysPieShopAdvanced.WebApi", "BethanysPieShop.WebApi.csproj");

if (!File.Exists(apiProject))
{
    throw new FileNotFoundException($"Web API project not found at '{apiProject}'.", apiProject)
    {
        HResult = 404,
        Source = "BethanysPieShop.AppHost"
    };
}

// Add Admin (Razor Pages) as a project resource
var admin = builder.AddProject("admin", adminProject)
    .WithEnvironment("ConnectionStrings__DefaultConnection", localConn);

// Add WebApi as a project resource
var api = builder.AddProject("webapi", apiProject)
    .WithEnvironment("ConnectionStrings__DefaultConnection", localConn);

// Optionally, express dependencies (e.g., admin depends on api)
admin.WithReference(api);

// Build and run the distributed application with DCP and Dashboard enabled
builder.Build().Run();

static string FindRoot(string start, string markerDir)
{
    var dir = new DirectoryInfo(start);
    while (dir != null && !Directory.Exists(Path.Combine(dir.FullName, markerDir)))
    {
        dir = dir.Parent!;
    }

    if (dir == null)
    {
        throw new DirectoryNotFoundException($"Could not locate '{markerDir}' starting at '{start}'.")
        {
            HResult = 404,
            Source = "BethanysPieShop.AppHost"
        };
    }

    return dir.FullName;
}