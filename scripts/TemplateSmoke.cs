using System.Text.Json.Nodes;
using Base.Core.Domains.Contracts.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TradingSmoke.Core.Contracts.Common;
using TradingSmoke.Endpoints.API.Extensions;
using TradingSmoke.Infrastructure.Data.Sql.Commands.Common;
using TradingSmoke.Infrastructure.Data.Sql.Queries.Common;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions {
    ContentRootPath = args[0], EnvironmentName = "Development",
    ApplicationName = typeof(HostingExtensions).Assembly.GetName().Name
});
builder.Logging.ClearProviders();
builder.WebHost.UseUrls("http://127.0.0.1:0");
builder.Configuration.AddInMemoryCollection(new Dictionary<string,string?> {
    ["OAuth:Enabled"] = "false", ["OpenApi:Enabled"] = "true"
});
await using var app = builder.ConfigureServices().ConfigurePipeline([]);
var checks = 0;
using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    Require(services.GetRequiredService<IMediator>() is not null, "MediatR registration");
    Require(ReferenceEquals(services.GetRequiredService<IUnitOfWork>(), services.GetRequiredService<ITradingSmokeUnitOfWork>()), "Shared unit of work");
    Require(ReferenceEquals(services.GetRequiredService<DbContext>(), services.GetRequiredService<TradingSmokeCommandDbContext>()), "Command DbContext binding");
    Require(services.GetRequiredService<TradingSmokeQueryDbContext>().ChangeTracker.QueryTrackingBehavior == QueryTrackingBehavior.NoTracking, "Read-only query context");
}
await app.StartAsync();
try {
    var address = app.Services.GetRequiredService<IServer>().Features.Get<IServerAddressesFeature>()!.Addresses.Single();
    using var client = new HttpClient { BaseAddress = new Uri(address) };
    var json = await client.GetStringAsync("/openapi/v1.json");
    var document = JsonNode.Parse(json)!;
    Require(document["paths"]!["/api/People/Create"] is not null, "Generated controller in OpenAPI");
    Require((await client.GetStringAsync("/scalar")).Contains("scalar", StringComparison.OrdinalIgnoreCase), "Scalar page");
    var result = await client.PostAsync("/api/People/Create", null);
    Require(result.IsSuccessStatusCode, "Controller activation and response");
    Require(JsonNode.Parse(await result.Content.ReadAsStringAsync())!["isSuccess"]!.GetValue<bool>(), "Base API result filter");
    Require((await client.GetAsync("/swagger/v1/swagger.json")).StatusCode == System.Net.HttpStatusCode.NotFound, "No legacy Swagger route");
} finally { await app.StopAsync(); }
Console.WriteLine($"Generated template startup/DI/HTTP smoke passed: {checks} assertions; no database connection or migration executed.");
void Require(bool condition, string message) { if (!condition) throw new Exception(message); checks++; }
