using Base.EndPoints.Web.Extensions.DependencyInjection;
using Base.Extensions.DependencyInjection;
using Base.Infra.Data.Sql.Commands.Interceptors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BaseTemplate.Infra.Data.Sql.Commands.Common;
using BaseTemplate.Infra.Data.Sql.Queries.Common;
using Base.EndPoints.Web.Extensions.ModelBinding;
using Serilog;

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
        builder.Services.AddDbContext<DbContextNameCommandDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString"))
            .AddInterceptors(new SetPersianYeKeInterceptor(), new AddAuditDataInterceptor()));
        builder.Services.AddDbContext<DbContextNameQueryDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("QueryDb_ConnectionString")));
        builder.Services.AddBasePollingPublisherDalSql(configuration, "PollingPublisherSqlStore");
        builder.Services.AddBaseMessageInboxDalSql(configuration, "MessageInboxSqlStore");
        builder.Services.AddSwaggerGen();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseBaseApiExceptionHandler();
        app.UseSerilogRequestLogging();
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