using Microsoft.EntityFrameworkCore;
using Base.Infra.Data.Sql.Queries;

namespace BaseTemplate.Infra.Data.Sql.Queries.Common;

public class DbContextNameQueryDbContext(DbContextOptions<DbContextNameQueryDbContext> options)
    : BaseQueryDbContext(options);