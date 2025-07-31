namespace BaseTemplate.Endpoints.API.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        IConfiguration configuration = builder.Configuration;
        builder.Services.AddBaseApiCore("Base", "BaseTemplate");
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddBaseWebUserInfoService(configuration, "WebUserInfo", true);
        builder.Services.AddBaseParrotTranslator(configuration, "ParrotTranslator");
        builder.Services.AddNonValidatingValidator();
        builder.Services.AddBaseMicrosoftSerializer();
        builder.Services.AddBaseAutoMapperProfiles(option =>
        {
            option.AssemblyNamesForLoadProfiles = "BaseTemplate";
        });

        builder.Services.AddBaseInMemoryCaching();
        builder.Services.AddDbContext<BaseTemplateCommandDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString"))
            .AddInterceptors(new SetPersianYeKeInterceptor(), new AddAuditDataInterceptor()));
        builder.Services.AddDbContext<BaseTemplateQueryDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("QueryDb_ConnectionString")));
        builder.Services.AddBasePollingPublisherDalSql(configuration, "PollingPublisherSqlStore");
        builder.Services.AddBaseMessageInboxDalSql(configuration, "MessageInboxSqlStore");
        builder.Services.AddSwaggerGen();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseBaseApiExceptionHandler();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseStatusCodePages();
        app.UseCors(delegate (CorsPolicyBuilder builder)
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
        app.UseHttpsRedirection();
        var controllerBuilder = app.MapControllers();
        return app;
    }
}