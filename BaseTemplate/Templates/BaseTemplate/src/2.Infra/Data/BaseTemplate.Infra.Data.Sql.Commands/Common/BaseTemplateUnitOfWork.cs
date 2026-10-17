using Base.Infra.Data.Sql.Commands;
using BaseTemplate.Core.Contracts.Common;

namespace BaseTemplate.Infra.Data.Sql.Commands.Common;

public class BaseTemplateUnitOfWork(BaseTemplateCommandDbContext dbContext) :
    BaseEntityFrameworkUnitOfWork<BaseTemplateCommandDbContext>(dbContext), IBaseTemplateUnitOfWork
{

}