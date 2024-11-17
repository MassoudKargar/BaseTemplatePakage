namespace BaseTemplate.Infrastructure.Common;

public class BaseTemplateDbContext : BaseDbContext
{
    public BaseTemplateDbContext(DbContextOptions<BaseTemplateDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}