using Microsoft.Extensions.Logging;

namespace Cadenza.Web.Actions.Effects;

public class DatabaseConnectionEffects
{
    private readonly IApiConnector _connector;
    private readonly IState<DatabaseConnectionState> _state;
    private readonly ILogger<DatabaseConnectionEffects> _logger;

    public DatabaseConnectionEffects(IState<DatabaseConnectionState> state, ILogger<DatabaseConnectionEffects> logger, IApiConnector connector)
    {
        _state = state;
        _logger = logger;
        _connector = connector;
    }

    [EffectMethod(typeof(DatabaseConnectRequest))]
    public async Task HandleDatabaseConnectRequest(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        try
        {
            await _connector.Connect();
            dispatcher.Dispatch(new DatabaseConnectedAction());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to database");
            dispatcher.Dispatch(new DatabaseConnectionFailedAction());
        }
    }

    [EffectMethod(typeof(DatabaseConnectionFailedAction))]
    public Task HandleDatabaseConnectionFailedAction(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        return Task.CompletedTask;
    }

    [EffectMethod(typeof(DatabaseConnectedAction))]
    public Task HandleDatabaseConnectedAction(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        dispatcher.Dispatch(new SearchItemsUpdateRequest());
        dispatcher.Dispatch(new FetchGroupingsRequest());
        dispatcher.Dispatch(new FetchPlaylistHistoryAlbumsRequest());
        dispatcher.Dispatch(new FetchPlaylistHistoryTagsRequest());
        return Task.CompletedTask;
    }

    private void DispatchProgressAction(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ApplicationStartupProgressAction(ConnectionType.Database, _state.Value.State, _state.Value.Message));
    }
}
