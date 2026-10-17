namespace BaseTemplate.Endpoints.API.CustomDecorators;

public class CustomQueryDecorator : QueryDispatcherDecorator
{
    public override int Order => 0;

    public override async Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query,CancellationToken cancellationToken)
    {
        return await _queryDispatcher.Execute<TQuery, TData>(query,cancellationToken);
    }
}