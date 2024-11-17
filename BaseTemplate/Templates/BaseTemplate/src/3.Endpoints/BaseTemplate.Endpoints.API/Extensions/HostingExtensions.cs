﻿namespace BaseTemplate.Endpoints.API.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        IConfiguration configuration = builder.Configuration;
        builder.Services.AddBaseApiCore("Base", "BaseTemplate");
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddBaseWebUserInfoService(configuration, "WebUserInfo", true);
        builder.Services.AddNonValidatingValidator();
        builder.Services.AddBaseMicrosoftSerializer();
        builder.Services.AddBaseAutoMapperProfiles(option =>
        {
            option.AssemblyNamesForLoadProfiles = "BaseTemplate";
        });

        builder.Services.AddDbContext<BaseDbContext, BaseTemplateDbContext>(c =>
            c.UseSqlServer(configuration.GetConnectionString("BaseConnectionString"), optionsBuilder =>
            {
                optionsBuilder.MigrationsAssembly(typeof(BaseTemplateDbContext).Assembly.GetName().Name);
            }));

        builder.Services.AddSwaggerGen();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseCustomExceptionHandler();
        app.UseSerilogRequestLogging();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseStatusCodePages();
        app.UseStaticFiles();
        app.UseCors(delegate (CorsPolicyBuilder builder)
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<BaseTemplateDbContext>();
            if (!dbContext.Database.CanConnect())
            {
                dbContext.Database.MigrateAsync();
            }
        }
        app.UseHttpsRedirection();
        var controllerBuilder = app.MapControllers();
        return app;
    }
}