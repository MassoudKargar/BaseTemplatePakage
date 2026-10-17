namespace BaseTemplate.Endpoints.API.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        IConfiguration configuration = builder.Configuration;
        builder.AddObservability();
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

        builder.Services.AddIdentityServer(configuration, "OAuth");

        builder.Services.AddOpenApiDocumentation(configuration, "OpenApi");
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app, string[] args)
    {
        var runMigrations = args.Contains("--migrate");
        if (runMigrations)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<BaseTemplateCommandDbContext>();
            db.Database.Migrate();
        }
        app.UseBaseObservabilityMiddleware();
        app.UseBaseApiExceptionHandler();
        app.UseOpenApiDocumentation("OpenApi");
        app.UseStatusCodePages();
        app.UseCors(delegate (CorsPolicyBuilder builder)
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
        app.UseHttpsRedirection();
        app.UseIdentityServer("OAuth");
        app.MapControllers();
        return app;
    }
}
