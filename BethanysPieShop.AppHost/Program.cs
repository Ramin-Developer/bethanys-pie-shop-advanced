var builder = DistributedApplication.CreateBuilder(args);

// SQL Server connection string (Windows auth)
// - Ensure the target database exists and is accessible.
// - The Admin Razor Pages app should read "ConnectionStrings:DefaultConnection"
//   and use builder.Configuration.GetConnectionString("DefaultConnection").
var localConn =
    "Server=localhost;Database=pieshopdb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True";

// Basic validation to fail fast on empty connection strings.
// (Prevents orchestrating a misconfigured app that will crash at runtime.)
if (string.IsNullOrWhiteSpace(localConn))
{
    throw new ArgumentException("The SQL connection string 'localConn' is empty.", nameof(localConn));
}

// Resolve repo root and Admin project path
// - Find the repository directory containing "BethanysPieShopAdvanced.Admin".
// - Compose the absolute path to the Admin .csproj used by Aspire orchestration.
var repoRoot = FindRoot(AppContext.BaseDirectory, "BethanysPieShopAdvanced.Admin");
var adminProject = Path.Combine(repoRoot, "BethanysPieShopAdvanced.Admin", "BethanysPieShop.Admin.csproj");

// Fail fast if the Admin project path is wrong or missing.
// (Avoids confusing runtime errors inside the orchestrator.)
if (!File.Exists(adminProject))
{
    throw new FileNotFoundException($"Admin project not found at '{adminProject}'.", adminProject)
    {
        HResult = 404,
        Source = "BethanysPieShop.AppHost"
    };
}

// Orchestrate Admin app and inject connection string
// - "ConnectionStrings__DefaultConnection" maps to "ConnectionStrings:DefaultConnection" in the Admin app.
// - The Admin app should resolve this via Configuration.GetConnectionString("DefaultConnection").
builder.AddProject("web", adminProject)
    .WithEnvironment("ConnectionStrings__DefaultConnection", localConn)
    // Optional: if your Aspire version supports exposing endpoints, uncomment below.
    // .WithExternalHttpEndpoints()
    ;

// Build and run the distributed application orchestration.
builder.Build().Run();

static string FindRoot(string start, string markerDir)
{
    // Walk up parent directories until a folder containing 'markerDir' is found.
    // This allows running the AppHost from different locations without hardcoding absolute paths.
    var dir = new DirectoryInfo(start);
    while (dir != null && Directory.Exists(Path.Combine(dir.FullName, markerDir)) == false)
        dir = dir.Parent!;
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