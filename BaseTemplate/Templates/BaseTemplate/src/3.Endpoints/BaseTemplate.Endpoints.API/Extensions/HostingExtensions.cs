namespace BaseTemplate.Endpoints.API.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
        builder.Services.AddCors();
        builder.Services.AddDefaultFrameworkServices(configuration);
        builder.Services.AddClock();
        builder.Services.AddMediatRServices(typeof(HostingExtensions).Assembly,
            typeof(IBaseTemplateUnitOfWork).Assembly,
            typeof(BaseTemplate.Core.ApplicationService.AssemblyMarker).Assembly);
        builder.Services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));
        builder.Services.AddDbContext<BaseTemplateCommandDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString")).AddPersianYeKeCommandInterceptor());
        builder.Services.AddDbContext<BaseTemplateQueryDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("QueryDb_ConnectionString")));
        builder.Services.AddScoped<DbContext>(services => services.GetRequiredService<BaseTemplateCommandDbContext>());
        builder.Services.AddScoped<BaseTemplateUnitOfWork>();
        builder.Services.AddScoped<IBaseTemplateUnitOfWork>(services => services.GetRequiredService<BaseTemplateUnitOfWork>());
        builder.Services.AddScoped<IUnitOfWork>(services => services.GetRequiredService<BaseTemplateUnitOfWork>());
        builder.Services.AddScoped<BaseTemplateCommandDbContextFactory>();
        builder.Services.AddIdentityServer(configuration, "OAuth");
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app, string[] args)
    {
        if (args.Contains("--migrate"))
        {
            using var scope = app.Services.CreateScope();
            scope.ServiceProvider.GetRequiredService<BaseTemplateCommandDbContext>().Database.Migrate();
        }
        app.UseExceptionAdapter();
        app.UseOpenApiDocumentation("OpenApi");
        app.UseStatusCodePages();
        app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        app.UseHttpsRedirection();
        app.UseIdentityServer("OAuth");
        app.MapControllers();
        return app;
    }
}
