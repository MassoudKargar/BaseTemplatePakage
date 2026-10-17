# Agent Instructions

These instructions apply to the entire repository.

## Repository Purpose

This repository builds a NuGet template package named `Base.Solution.Templates`.
The package project is `BaseTemplate/BaseTemplate.csproj` and it packages the
solution template under `BaseTemplate/templates/BaseTemplate`.

## Layout

- `BaseTemplatePakage.sln`: outer solution for building and packing the template package.
- `BaseTemplate/BaseTemplate.csproj`: template package project. It includes all files under `templates/**` except `bin` and `obj`.
- `BaseTemplate/templates/BaseTemplate/BaseTemplate.sln`: the solution emitted by the template.
- `BaseTemplate/templates/BaseTemplate/src/1.Core`: domain, contracts, application service, request/response, and resource projects.
- `BaseTemplate/templates/BaseTemplate/src/2.Infrastructure`: SQL command/query infrastructure projects.
- `BaseTemplate/templates/BaseTemplate/src/3.Endpoints`: ASP.NET Core API endpoint project.
- `nupkgs/`: generated NuGet packages. Treat as build output unless the user explicitly asks to inspect package artifacts.

## Build And Verification Commands

Run commands from the repository root unless noted otherwise.

- Restore/package project: `dotnet restore BaseTemplatePakage.sln`
- Build/package project: `dotnet build BaseTemplatePakage.sln`
- Pack template package: `dotnet pack BaseTemplate/BaseTemplate.csproj -o nupkgs`
- Alternative package script on Windows: `build-package.bat`
- Restore nested template solution: `dotnet restore BaseTemplate/templates/BaseTemplate/BaseTemplate.sln`
- Build nested template solution: `dotnet build BaseTemplate/templates/BaseTemplate/BaseTemplate.sln`
- Run API template project:
  `dotnet run --project BaseTemplate/templates/BaseTemplate/src/3.Endpoints/BaseTemplate.Endpoints.API/BaseTemplate.Endpoints.API.csproj`
- Run API template project with migrations:
  `dotnet run --project BaseTemplate/templates/BaseTemplate/src/3.Endpoints/BaseTemplate.Endpoints.API/BaseTemplate.Endpoints.API.csproj -- --migrate`

There are currently no test projects in the repository. When adding tests, place
them in the nested template solution and include them in
`BaseTemplate/templates/BaseTemplate/BaseTemplate.sln`.

## Framework And Dependencies

- The package project currently targets `net10.0`.
- The nested template projects currently target `net10.0`.
- The template uses the `Base.*` framework packages, Entity Framework Core,
  ASP.NET Core, SQL Server, native OpenAPI/Scalar, MediatR, and
  the packages built from the BaseDotNet10 branch in C:\Base\sec.
- There is no `global.json`; use the installed SDK that can build the target
  frameworks unless the user asks to pin one.

## Coding Conventions

- Preserve the layered structure: Core projects should not depend on Endpoint or
  Infrastructure projects; Endpoint composes services and infrastructure.
- Keep namespaces aligned with the project and folder names, using
  `BaseTemplate.*` for template code.
- Prefer file-scoped namespaces, global usings, and primary constructors where
  the existing code already uses them.
- Register cross-cutting services in
  `src/3.Endpoints/BaseTemplate.Endpoints.API/Extensions/HostingExtensions.cs`.
- Keep MediatR pipeline customizations in
  `src/3.Endpoints/BaseTemplate.Endpoints.API/Extensions/RequestLoggingBehavior.cs`.
- Keep EF Core command-side context, unit of work, schemas, factories, and
  migrations under
  `src/2.Infrastructure/Data/BaseTemplate.Infrastructure.Data.Sql.Commands`.
- Keep query-side DbContext code under
  `src/2.Infrastructure/Data/BaseTemplate.Infrastructure.Data.Sql.Queries`.

## Template-Specific Rules

- Remember that files under `BaseTemplate/templates/BaseTemplate` are template
  source files. Changes there affect projects generated from this package.
- Do not rename `BaseTemplate` placeholders casually; they are part of the
  template identity and package content.
- Avoid committing generated `bin`, `obj`, or `nupkgs` output unless the user
  explicitly requests package artifacts.
- Be careful with path casing. Git status may show both `Templates` and
  `templates` on case-insensitive Windows file systems. Preserve the existing
  on-disk path `BaseTemplate/templates` when adding or editing files.
- Do not overwrite user changes in the dirty worktree. Inspect relevant files
  before editing, and keep edits narrowly scoped.

## Configuration Notes

- The API template uses local SQL Server connection strings in `appsettings.json`.
  Do not treat the checked-in values as production secrets; they are template
  defaults.
- The `--migrate` runtime argument triggers EF Core migration execution in the
  API startup pipeline.
- OAuth and OpenAPI settings
  are configured through the endpoint project's `appsettings*.json` files.
