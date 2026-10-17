using Base.Core.Domains.Contracts.UnitOfWorks;
using Base.Infrastructure.Persistence.EntityFramework;
using BaseTemplate.Core.Contracts.Common;

namespace BaseTemplate.Infrastructure.Data.Sql.Commands.Common;

public class BaseTemplateUnitOfWork(BaseTemplateCommandDbContext dbContext, IEnumerable<IUnitOfWorkInterceptor> interceptors)
    : UnitOfWork(dbContext, interceptors), IBaseTemplateUnitOfWork;
