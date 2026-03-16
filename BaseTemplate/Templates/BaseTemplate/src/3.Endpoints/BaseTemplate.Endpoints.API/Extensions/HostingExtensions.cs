using Base.Core.ApplicationServices.Commands;
using Base.Core.ApplicationServices.Events;
using Base.Core.ApplicationServices.Queries;

using BaseTemplate.Endpoints.API.CustomDecorators;
using BaseTemplate.Endpoints.API.Extensions.DependencyInjection.IdentityServer.Extensions;

using FluentValidation.AspNetCore;

using Microsoft.OpenApi;

using Scalar.AspNetCore;

namespace BaseTemplate.Endpoints.API.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        IConfiguration configuration = builder.Configuration;
        builder.Services.AddSingleton<CommandDispatcherDecorator, CustomCommandDecorator>();
        builder.Services.AddSingleton<QueryDispatcherDecorator, CustomQueryDecorator>();
        builder.Services.AddSingleton<EventDispatcherDecorator, CustomEventDecorator>();
        builder.Services.AddBaseApiCore("Base", "BaseTemplate");
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddBaseWebUserInfoService(configuration, "WebUserInfo", true);
        builder.Services.AddBaseParrotTranslator(configuration, "ParrotTranslator");
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddBaseMicrosoftSerializer();
        builder.Services.AddBaseAutoMapperProfiles(option =>
        {
            option.AssemblyNamesForLoadProfiles = "BaseTemplate";
        });

        builder.Services.AddBaseInMemoryCaching();
        builder.Services.AddDbContext<BaseTemplateCommandDbContext>(c => c
            .UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString"))
            .AddInterceptors(new SetPersianYeKeInterceptor(), new AddAuditDataInterceptor()));
        builder.Services.AddDbContext<BaseTemplateQueryDbContext>(c =>
            c.UseSqlServer(configuration.GetConnectionString("QueryDb_ConnectionString")));
        builder.Services.AddScoped<BaseTemplateCommandDbContextFactory>();
        builder.Services.AddBasePollingPublisherDalSql(configuration, "PollingPublisherSqlStore");
        builder.Services.AddBaseMessageInboxDalSql(configuration, "MessageInboxSqlStore");
        builder.Services.AddIdentityServer(configuration, "OAuth");
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        // Register only OpenAPI (no Swashbuckle/Swagger services)
        builder.Services.AddOpenApi(options =>
        {
            options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
        });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseBaseObservabilityMiddleware();
        app.UseBaseApiExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            // Use OpenAPI-only tooling in development. Do not enable Swashbuckle/Swagger middleware.
            // map scalar API reference UI to /scalar
            app.MapScalarApiReference(options =>
            {
                options.WithTitle("Medication Schedule API")
                    .WithTheme(ScalarTheme.DeepSpace)
                    .ShowOperationId()
                    .ExpandAllTags()
                    .SortTagsAlphabetically()
                    .SortOperationsByMethod()
                    .PreserveSchemaPropertyOrder();
            });
        }
        app.UseStatusCodePages();
        app.UseCors(delegate (CorsPolicyBuilder builder)
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
        app.UseHttpsRedirection();
        app.UseAuthentication();

        var useIdentityServer = app.UseIdentityServer("OAuth");

        if (useIdentityServer)
        {
            app.MapControllers().RequireAuthorization();
        }
        else
        {
            app.MapControllers();
        }
        return app;
    }
}