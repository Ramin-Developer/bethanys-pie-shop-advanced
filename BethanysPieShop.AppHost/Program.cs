var localConn =
    "Server=localhost;Database=pieshopdb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True";

if (string.IsNullOrWhiteSpace(localConn))
{
    throw new ArgumentException("The SQL connection string 'localConn' is empty.", nameof(localConn));
}

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

Console.WriteLine($"Admin project located at: {adminProject}");
Console.WriteLine("AppHost no longer uses Aspire.Hosting; custom orchestration can go here.");

static string FindRoot(string start, string markerDir)
{
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