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

        //Base
        builder.Services.AddBaseApiCore("Base", "BaseTemplate");

        //microsoft
        builder.Services.AddEndpointsApiExplorer();

        //Base
        builder.Services.AddBaseWebUserInfoService(configuration, "WebUserInfo", true);

        //Base
        builder.Services.AddBaseParrotTranslator(configuration, "ParrotTranslator");

        //Base
        //builder.Services.AddSoftwarePartDetector(configuration, "SoftwarePart");

        //Base
        builder.Services.AddNonValidatingValidator();

        //Base
        builder.Services.AddBaseMicrosoftSerializer();

        //Base
        builder.Services.AddBaseAutoMapperProfiles(option =>
        {
            option.AssemblyNamesForLoadProfiles = "Base";
        });


        //Base
        builder.Services.AddBaseInMemoryCaching();
        //builder.Services.AddBaseSqlDistributedCache(configuration, "SqlDistributedCache");

        //CommandDbContext
        builder.Services.AddDbContext<DbContextNameCommandDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString"))
            .AddInterceptors(new SetPersianYeKeInterceptor(), new AddAuditDataInterceptor()));

        //QueryDbContext
        builder.Services.AddDbContext<DbContextNameQueryDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("QueryDb_ConnectionString")));

        //PollingPublisher
        builder.Services.AddBasePollingPublisherDalSql(configuration, "PollingPublisherSqlStore");
        //builder.Services.AddBasePollingPublisher(configuration, "PollingPublisher");

        //MessageInbox
        builder.Services.AddBaseMessageInboxDalSql(configuration, "MessageInboxSqlStore");
        //builder.Services.AddBaseMessageInbox(configuration, "MessageInbox");

        //builder.Services.AddBaseRabbitMqMessageBus(configuration, "RabbitMq");

        //builder.Services.AddBaseTraceJeager(configuration, "OpenTeletmetry");

        builder.Services.AddSwaggerGen();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        //Base
        app.UseBaseApiExceptionHandler();

        //Serilog
        app.UseSerilogRequestLogging();

        // Configure the HTTP request pipeline.
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

        //app.Services.ReceiveEventFromRabbitMqMessageBus(new KeyValuePair<string, string>("MiniAggregateName", "AggregateNameCreated"));

        //var useIdentityServer = app.UseIdentityServer("OAuth");

        var controllerBuilder = app.MapControllers();

        //if (useIdentityServer)
        //    controllerBuilder.RequireAuthorization();

        //app.Services.GetService<SoftwarePartDetectorService>()?.Run();

        return app;
    }
}