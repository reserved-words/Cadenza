namespace Cadenza.State.Actions.Effects;

public class ApplicationEffects
{
    private readonly IStartupDialogService _dialogService;

    private readonly IState<DatabaseConnectionState> _databaseConnectionState;
    private readonly IState<LastFmConnectionState> _lastFmConnectionState;
    private readonly IState<LocalSourceConnectionState> _localSourceConnectionState;

    public ApplicationEffects(IStartupDialogService dialogService, IState<DatabaseConnectionState> databaseConnectionState, IState<LastFmConnectionState> lastFmConnectionState, IState<LocalSourceConnectionState> localSourceConnectionState)
    {
        _dialogService = dialogService;
        _databaseConnectionState = databaseConnectionState;
        _lastFmConnectionState = lastFmConnectionState;
        _localSourceConnectionState = localSourceConnectionState;
    }


    [EffectMethod(typeof(ApplicationStartRequest))]
    public async Task HandleApplicationStartRequest(IDispatcher dispatcher)
    {
        var connections = new List<ConnectionStartupParameter>
        {
            new ConnectionStartupParameter(ConnectionType.Database, new DatabaseConnectRequest()),
            new ConnectionStartupParameter(ConnectionType.Local, new LocalSourceConnectRequest()),
            new ConnectionStartupParameter(ConnectionType.LastFm, new LastFmConnectRequest())
        };

        await _dialogService.Run(connections);
    }

    [EffectMethod(typeof(DatabaseConnectedAction))]
    public Task HandleDatabaseConnectedAction(IDispatcher dispatcher)
    {
        return CheckIfAllConnected(dispatcher);
    }

    [EffectMethod(typeof(LastFmConnectedAction))]
    public Task HandleLastFmConnectedAction(IDispatcher dispatcher)
    {
        return CheckIfAllConnected(dispatcher);
    }

    [EffectMethod(typeof(LocalSourceConnectedAction))]
    public Task HandleLocalSourceConnectedAction(IDispatcher dispatcher)
    {
        return CheckIfAllConnected(dispatcher);
    }

    private Task CheckIfAllConnected(IDispatcher dispatcher)
    {
        if (_databaseConnectionState.Value.State == ConnectionState.Connected
            && _lastFmConnectionState.Value.State == ConnectionState.Connected
            && _localSourceConnectionState.Value.State == ConnectionState.Connected)
        {
            dispatcher.Dispatch(new ApplicationStartedAction());
        }

        return Task.CompletedTask;
    }
}
