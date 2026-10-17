namespace BaseTemplate.Infrastructure.Data.Sql.Queries.Common;

public class BaseTemplateQueryDbContext(DbContextOptions<BaseTemplateQueryDbContext> options)
    : QueryDbContextBase(options);
