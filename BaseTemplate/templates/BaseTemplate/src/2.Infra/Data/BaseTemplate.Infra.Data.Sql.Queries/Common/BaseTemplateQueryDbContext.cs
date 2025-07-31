namespace BaseTemplate.Infra.Data.Sql.Queries.Common;

public class BaseTemplateQueryDbContext(DbContextOptions<BaseTemplateQueryDbContext> options)
    : BaseQueryDbContext(options);