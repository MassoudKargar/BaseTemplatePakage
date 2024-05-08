using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BaseTemplate.Infra.Data.Sql.Commands.Common;

public class DbContextNameCommandDbContextFactory : IDesignTimeDbContextFactory<DbContextNameCommandDbContext>
{
    public DbContextNameCommandDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<DbContextNameCommandDbContext>();

        builder.UseSqlServer("Server=.; Database=DbContextNameDb;User Id =masoud;Password=M@$$0ud1001; MultipleActiveResultSets=true; Encrypt=false");

        return new DbContextNameCommandDbContext(builder.Options);
    }
}