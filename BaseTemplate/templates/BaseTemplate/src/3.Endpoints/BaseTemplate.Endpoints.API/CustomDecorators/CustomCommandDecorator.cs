namespace BaseTemplate.Endpoints.API.CustomDecorators;

public class CustomCommandDecorator : CommandDispatcherDecorator
{
    public override int Order => 0;

    public override async Task<CommandResult> Send<TCommand>(TCommand command,CancellationToken cancellationToken)
    {
        return await _commandDispatcher.Send(command,cancellationToken);
    }

    public override async Task<CommandResult<TData>> Send<TCommand, TData>(TCommand command,CancellationToken cancellationToken)
    {
        return await _commandDispatcher.Send<TCommand, TData>(command,cancellationToken);
    }
}