namespace BaseTemplate.Infrastructure.Data.Sql.Commands.Common;

public class BaseTemplateCommandDbContextFactory : IDesignTimeDbContextFactory<BaseTemplateCommandDbContext>
{
    public BaseTemplateCommandDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("CommandDb_ConnectionString") ?? throw new NullReferenceException("CommandDb_ConnectionString is null");
        var builder = new DbContextOptionsBuilder<BaseTemplateCommandDbContext>();
        builder.UseSqlServer(connectionString);

        return new BaseTemplateCommandDbContext(builder.Options);
    }
}