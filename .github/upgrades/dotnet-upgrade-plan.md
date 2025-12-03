# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade BethanysPieShopAdvanced.Common\BethanysPieShop.Common.csproj
4. Upgrade BethanysPieShopAdvanced.DataAccess\BethanysPieShop.DataAccess.csproj
5. Upgrade BethanysPieShopAdvanced.BusinessLogic\BethanysPieShop.BusinessLogic.csproj
6. Upgrade BethanysPieShopAdvanced.Shared\BethanysPieShop.Shared.csproj
7. Upgrade BethanysPieShopAdvanced.WebApi\BethanysPieShop.WebApi.csproj
8. Upgrade BethanysPieShop.IntegrationTests\BethanysPieShop.IntegrationTests.csproj
9. Run unit tests to validate upgrade in the projects listed below:
   - BethanysPieShop.IntegrationTests\BethanysPieShop.IntegrationTests.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

Table below contains projects that do belong to the dependency graph for selected projects and should not be included in the upgrade.

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                                  | Current Version            | New Version | Description                                          |
|:----------------------------------------------|:--------------------------:|:-----------:|:-----------------------------------------------------|
| Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore | 8.0.5                   | 10.0.0      | Recommended for .NET 10.0                            |
| Microsoft.AspNetCore.Mvc.Testing              | 8.0.5                      | 10.0.0      | Recommended for .NET 10.0                            |
| Microsoft.AspNetCore.OpenApi                  | 8.0.5                      | 10.0.0      | Recommended for .NET 10.0                            |
| Microsoft.EntityFrameworkCore                 | 8.0.5                      | 10.0.0      | Recommended for .NET 10.0                            |
| Microsoft.EntityFrameworkCore.Design          | 8.0.5                      | 10.0.0      | Recommended for .NET 10.0                            |
| Microsoft.EntityFrameworkCore.InMemory        | 8.0.5                      | 10.0.0      | Recommended for .NET 10.0                            |
| Microsoft.EntityFrameworkCore.SqlServer       | 8.0.5                      | 10.0.0      | Recommended for .NET 10.0                            |
| Microsoft.EntityFrameworkCore.Tools           | 8.0.5                      | 10.0.0      | Recommended for .NET 10.0                            |
| Microsoft.Extensions.Caching.Memory           | 8.0.0                      | 10.0.0      | Security vulnerability fix and .NET 10.0 recommended |
| Microsoft.Extensions.DependencyInjection.Abstractions | 8.0.1                | 10.0.0      | Recommended for .NET 10.0                            |
| Swashbuckle.AspNetCore                        | 6.6.2                      | 6.7.0       | Latest compatible                                    |
| Microsoft.IdentityModel.JsonWebTokens         | 7.6.0                      | 8.15.0      | Move to LTS per vendor guidance                      |
| System.IdentityModel.Tokens.Jwt               | 7.6.0                      | 8.15.0      | Move to LTS per vendor guidance                      |

### Project upgrade details
This section contains details about each project upgrade and modifications that need to be done in the project.

#### BethanysPieShopAdvanced.Common\\BethanysPieShop.Common.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - No changes

Other changes:
  - None

#### BethanysPieShopAdvanced.DataAccess\\BethanysPieShop.DataAccess.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.EntityFrameworkCore should be updated from `8.0.5` to `10.0.0` (recommended for .NET 10.0)
  - Microsoft.EntityFrameworkCore.Design should be updated from `8.0.5` to `10.0.0` (recommended for .NET 10.0)
  - Microsoft.EntityFrameworkCore.SqlServer should be updated from `8.0.5` to `10.0.0` (recommended for .NET 10.0)
  - Microsoft.EntityFrameworkCore.Tools should be updated from `8.0.5` to `10.0.0` (recommended for .NET 10.0)
  - Microsoft.IdentityModel.JsonWebTokens should be updated from `7.6.0` to `8.15.0` (move to LTS per vendor guidance)
  - System.IdentityModel.Tokens.Jwt should be updated from `7.6.0` to `8.15.0` (move to LTS per vendor guidance)

Other changes:
  - None

#### BethanysPieShopAdvanced.BusinessLogic\\BethanysPieShop.BusinessLogic.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - No changes

Other changes:
  - None

#### BethanysPieShopAdvanced.Shared\\BethanysPieShop.Shared.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.Extensions.DependencyInjection.Abstractions should be updated from `8.0.1` to `10.0.0` (recommended for .NET 10.0)

Other changes:
  - None

#### BethanysPieShopAdvanced.WebApi\\BethanysPieShop.WebApi.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.AspNetCore.OpenApi should be updated from `8.0.5` to `10.0.0` (recommended for .NET 10.0)
  - Microsoft.EntityFrameworkCore.Design should be updated from `8.0.5` to `10.0.0` (recommended for .NET 10.0)
  - Microsoft.EntityFrameworkCore.SqlServer should be updated from `8.0.5` to `10.0.0` (recommended for .NET 10.0)
  - Microsoft.Extensions.Caching.Memory should be updated from `8.0.0` to `10.0.0` (security vulnerability fix and .NET 10.0 recommended)
  - Swashbuckle.AspNetCore should be updated from `6.6.2` to `6.7.0` (latest compatible)

Other changes:
  - None

#### BethanysPieShop.IntegrationTests\\BethanysPieShop.IntegrationTests.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.AspNetCore.Mvc.Testing should be updated from `8.0.5` to `10.0.0` (recommended for .NET 10.0)
  - Microsoft.EntityFrameworkCore should be updated from `8.0.5` to `10.0.0` (recommended for .NET 10.0)
  - Microsoft.EntityFrameworkCore.InMemory should be updated from `8.0.5` to `10.0.0` (recommended for .NET 10.0)

Other changes:
  - None

#### BethanysPieShopAdvanced.Admin\\BethanysPieShop.Admin.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore should be updated from `8.0.5` to `10.0.0` (recommended for .NET 10.0)
  - Microsoft.EntityFrameworkCore.Design should be updated from `8.0.5` to `10.0.0` (recommended for .NET 10.0)

Other changes:
  - None
