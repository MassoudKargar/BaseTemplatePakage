namespace BaseTemplate.Infra.Data.Sql.Commands.Common;

public class BaseTemplateCommandDbContext(DbContextOptions<BaseTemplateCommandDbContext> options) : BaseOutboxCommandDbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}