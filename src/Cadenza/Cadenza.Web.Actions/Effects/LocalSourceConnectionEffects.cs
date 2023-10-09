using Microsoft.Extensions.Logging;

namespace Cadenza.Web.Actions.Effects;

public class LocalSourceConnectionEffects
{
    private readonly ILocalSourceConnector _connector;
    private readonly IState<LocalSourceConnectionState> _state;
    private readonly ILogger<LocalSourceConnectionEffects> _logger;

    public LocalSourceConnectionEffects(ILocalSourceConnector connector, IState<LocalSourceConnectionState> state, ILogger<LocalSourceConnectionEffects> logger)
    {
        _connector = connector;
        _state = state;
        _logger = logger;
    }

    [EffectMethod(typeof(LocalSourceConnectRequest))]
    public async Task HandleLocalSourceConnectRequest(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        try
        {
            await _connector.Connect();
            dispatcher.Dispatch(new LocalSourceConnectedAction());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error connecting to local source");
            dispatcher.Dispatch(new LocalSourceConnectionFailedAction());
        }
    }

    [EffectMethod(typeof(LocalSourceConnectionFailedAction))]
    public Task HandleLocalSourceConnectionFailedAction(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        return Task.CompletedTask;
    }

    [EffectMethod(typeof(LocalSourceConnectedAction))]
    public Task HandleLocalSourceConnectedAction(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        return Task.CompletedTask;
    }

    private void DispatchProgressAction(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ApplicationStartupProgressAction(ConnectionType.Local, _state.Value.State, _state.Value.Message));
    }
}
