using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Base.Extensions.Events.Outbox.Dal.EF;

namespace BaseTemplate.Infra.Data.Sql.Commands.Common;

public class DbContextNameCommandDbContext(DbContextOptions<DbContextNameCommandDbContext> options) : BaseOutboxCommandDbContext(options)
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