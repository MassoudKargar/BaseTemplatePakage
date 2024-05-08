using Base.Extensions.DependencyInjection;
using Base.Utilities.SerilogRegistration.Extensions;
using BaseTemplate.Endpoints.API.Extensions;

SerilogExtensions.RunWithSerilogExceptionHandling(() =>
{
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.AddBaseSerilog(o =>
    {
        o.ApplicationName = builder.Configuration.GetValue<string>("ApplicationName") ?? string.Empty;
        o.ServiceId = builder.Configuration.GetValue<string>("ServiceId") ?? string.Empty;
        o.ServiceName = builder.Configuration.GetValue<string>("ServiceName") ?? string.Empty;
        o.ServiceVersion = builder.Configuration.GetValue<string>("ServiceVersion") ?? string.Empty;
    }).ConfigureServices().ConfigurePipeline();
    app.Run();
});