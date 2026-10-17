namespace BaseTemplate.Infrastructure.Data.Sql.Commands.Common;

public class BaseTemplateCommandDbContext(DbContextOptions<BaseTemplateCommandDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
