# Bethany's Pie Shop — Advanced

[![CI](https://github.com/Ramin-Developer/bethanys-pie-shop-advanced/actions/workflows/ci.yml/badge.svg)](https://github.com/Ramin-Developer/bethanys-pie-shop-advanced/actions/workflows/ci.yml)

A layered **.NET 10** sample application for managing an online pie shop's catalog and orders, built to demonstrate clean architecture, modern ASP.NET Core, EF Core, a REST API, .NET Aspire orchestration, and integration testing.

> **Origin & attribution:** The domain and initial structure are based on Gill Cleeren's "Bethany's Pie Shop" training material. This repository extends that foundation with my own additions — a .NET 10 upgrade, a Web API layer, a .NET Aspire AppHost for orchestration, central package management, and an integration test suite. See [My Contributions](#my-contributions) below.

## Architecture

The solution follows a layered architecture with clear separation of concerns:

| Project | Responsibility |
| --- | --- |
| `BethanysPieShopAdvanced.Admin` | ASP.NET Core MVC admin UI (pies, categories, orders) |
| `BethanysPieShopAdvanced.WebApi` | REST API exposing pie/category resources |
| `BethanysPieShopAdvanced.BusinessLogic` | Service layer: validation, orchestration, DTO mapping |
| `BethanysPieShopAdvanced.DataAccess` | EF Core `DbContext`, repositories, entity configurations, migrations |
| `BethanysPieShopAdvanced.Shared` | Shared DTOs and mapping profiles |
| `BethanysPieShopAdvanced.Common` | Cross-cutting types (custom exceptions, helpers) |
| `BethanysPieShop.AppHost` | .NET Aspire orchestration host (dashboard + service coordination) |
| `BethanysPieShopAdvanced.IntegrationTests` | xUnit integration tests with `WebApplicationFactory` |

Flow: **UI / API → BusinessLogic (services) → DataAccess (repositories) → SQL Server (EF Core)**, with DTOs mapped via AutoMapper and errors surfaced through strongly-typed custom exceptions.

## Tech Stack

- **.NET 10**, C# (primary constructors, file-scoped namespaces, nullable reference types)
- **ASP.NET Core MVC** (Admin) and **Minimal/Controller Web API**
- **Entity Framework Core 10** (SQL Server)
- **AutoMapper** for entity ↔ DTO mapping
- **.NET Aspire** for local orchestration and the developer dashboard
**AwesomeAssertions** for integration tests
- **Central Package Management** (`Directory.Packages.props`)

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB or a local/remote instance)
- Visual Studio 2026 or the `dotnet` CLI

### Database setup

The connection string is configured in `BethanysPieShop.AppHost/appsettings.Development.json` (and the individual web projects). Apply the EF Core migrations:

```powershell
dotnet ef database update --project BethanysPieShopAdvanced.DataAccess --startup-project BethanysPieShopAdvanced.Admin
```

### Run with .NET Aspire (recommended)

Set `BethanysPieShop.AppHost` as the startup project and run it. The Aspire dashboard opens automatically:

```powershell
dotnet run --project BethanysPieShop.AppHost
```

The dashboard lists the Admin and Web API resources with their endpoints, logs, and traces.

### Run a single project

```powershell
dotnet run --project BethanysPieShopAdvanced.Admin
dotnet run --project BethanysPieShopAdvanced.WebApi
```

## Tests

```powershell
dotnet test
```

## My Contributions

Additions and modernizations I made on top of the original course material:

- Upgraded the entire solution to **.NET 10**.
- Added a **Web API** project (`BethanysPieShopAdvanced.WebApi`).
- Added a **.NET Aspire AppHost** for orchestration and observability, including correct DCP/Dashboard SDK wiring and launch profiles.
(AutoMapper) to its last free version; replaced FluentAssertions with the Apache-2.0 fork AwesomeAssertions.
- Built an **integration test suite** (happy-path and sad-path) with a custom `WebApplicationFactory` and database fixtures.
- Applied **custom generic exceptions** and consistent service-layer validation.

## License

This repository is for educational and portfolio purposes.
