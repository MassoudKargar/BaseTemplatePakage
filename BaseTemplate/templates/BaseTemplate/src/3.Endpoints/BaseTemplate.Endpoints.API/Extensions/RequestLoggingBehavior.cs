using MediatR;

namespace BaseTemplate.Endpoints.API.Extensions;

public sealed class RequestLoggingBehavior<TRequest, TResponse>(ILogger<RequestLoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogDebug("Handling request {RequestType}", typeof(TRequest).Name);
        return await next(cancellationToken);
    }
}
