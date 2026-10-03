# Testing Roadmap

Tracks the automated-testing work for the Bethany's Pie Shop Advanced solution:
what has been implemented and what remains.

## Status — You Are Here

- **Branch:** `main` (clean, synced with `origin/main`).
- **Last merged:** PR #12 — *CategoryService unit tests + testing roadmap* ✅ merged.
- **Test suite:** 23 unit tests passing (14 PieService + 9 CategoryService).
- **NEXT STEP:** Start **"Async-queryable unit tests"** (first item under *Planned / TODO*).
  Create a branch (e.g. `test/async-queryable-service-tests`), add a
  `TestAsyncQueryable`/`TestAsyncEnumerator` helper, then cover the listed
  `PieService` / `CategoryService` methods.

## Implemented

- [x] **Integration tests** (`BethanysPieShopAdvanced.IntegrationTests`) — end-to-end
  controller/API coverage against an in-memory database (`CustomWebApplicationFactory`).
- [x] **xUnit1051 compliance** — all async test calls pass
  `TestContext.Current.CancellationToken`.
- [x] **CA1816 compliance** — all `Dispose()`/`DisposeAsync()` methods call
  `GC.SuppressFinalize(this)`.
- [x] **Code hygiene** — redundant `using` directives removed, global usings consolidated
  into `Usings.cs`, primary constructors + file-scoped namespaces applied to test classes.
- [x] **Unit test project** (`BethanysPieShopAdvanced.UnitTests`, xUnit v3 + NSubstitute +
  AwesomeAssertions):
  - [x] `PieServiceTests` — `GetPieByIdAsync`, `GetPieByNameAsync`, `AddPieAsync`
	(null / empty / duplicate / valid), `GetPiesCountAsync`.
  - [x] `CategoryServiceTests` — `GetCategoriesAsync`, `GetCategoryByIdAsync`
	(validation / not-found / happy), `FindCategoryByTypeAsync`, `GetCategioryCountAsync`,
	`UpdateCategoryAsync` null guard, `DeleteCategoryAsync` not-found.
  - Current status: **23 tests passing**.

## Planned / TODO

- [ ] **Async-queryable unit tests** — add a `TestAsyncQueryable`/`TestAsyncEnumerator`
  helper so service methods that call `.ToListAsync()` / `.AnyAsync()` on repository
  `IQueryable<T>` can be unit-tested with NSubstitute. Targets:
  - `PieService.GetPiesAsync`, `GetPagedPiesAsync`, `GetSortedPaginatedPiesAsync`,
	`SearchPiesAsync`, `UpdatePieAsync`, `DeletePieAsync` (via `PieExistsAsync`).
  - `CategoryService.UpdateCategoryAsync` / `DeleteCategoryAsync` deeper branches.
- [ ] **Repository unit tests** — cover `PieRepository` sorting, paging, search predicate,
  and the `DbUpdateConcurrencyException` → returns 0 path using the EF Core InMemory provider.
- [ ] **Automatic code coverage** — wire up coverage collection and reporting:
  - Run `dotnet test --collect:"XPlat Code Coverage"` (coverlet.collector is already referenced).
  - Publish a coverage summary/artifact in the GitHub Actions CI workflow.
  - Optionally add a coverage threshold gate to fail the build below a target %.
- [ ] **Regression guard** — consider `TreatWarningsAsErrors` (or analyzer severity bumps)
  in CI so the xUnit1051/CA1816 fixes cannot silently regress.

## Notes

- Central package management is in `Directory.Packages.props`; add test package versions
  there and reference without versions in project files.
- Follow the solution convention: one `Usings.cs` per project for global usings,
  file-scoped namespaces, and primary constructors where applicable.
