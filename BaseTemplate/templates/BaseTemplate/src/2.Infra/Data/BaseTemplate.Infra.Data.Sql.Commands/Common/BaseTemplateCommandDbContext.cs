using Base.Infra.Data.Sql.Commands;

namespace BaseTemplate.Infra.Data.Sql.Commands.Common;

public class BaseTemplateCommandDbContext(DbContextOptions<BaseTemplateCommandDbContext> options) : BaseCommandDbContext(options)
{

    protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}