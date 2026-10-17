# Base Solution Templates

A .NET 10 solution template using the Base framework, with Core, SQL command/query infrastructure, and an ASP.NET Core API.

## Install and generate

```powershell
dotnet new install Base.Solution.Templates::10.0.0
dotnet new basetemplate -n MyApplication -o MyApplication
dotnet build MyApplication/MyApplication.sln
```

Requires a .NET 10 SDK and the Base package versions referenced by the generated projects. Publish the framework packages before this template. The template package does not bundle framework binaries.

Configure SQL Server, OAuth, and observability settings before running the API. Database migrations are opt-in through `--migrate` and use the command DbContext; do not use production credentials for a smoke test.

The template does not contain automated test projects. Building a generated application verifies compilation, not external-service connectivity.

API documentation uses native OpenAPI at `/openapi/v1.json` and Scalar at `/scalar`. Configure the `OpenApi` section for title, Bearer/OAuth2, public client ID, PKCE, and custom routes. `SwaggerDoc` settings remain supported for existing applications. Swashbuckle is not required by generated projects.
