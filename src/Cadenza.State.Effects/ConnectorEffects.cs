namespace Cadenza.State.Effects;

public class ConnectorEffects
{
    private readonly IDebugLogger _logger;

    public ConnectorEffects(IDebugLogger logger)
    {
        _logger = logger;
    }

    [EffectMethod]
    public async Task HandleConnectorStatusUpdateRequest_Error(ConnectorStatusUpdateRequest action, IDispatcher dispatcher)
    {
        if (action.Error != null)
        {
            await _logger.LogError(action.Error);
        }
    }

    [EffectMethod]
    public Task HandleConnectorStatusUpdateRequest(ConnectorStatusUpdateRequest action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ConnectorStatusUpdatedAction(action.Connector, action.Status));
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task HandleConnectorStatusUpdatedAction(ConnectorStatusUpdatedAction action, IDispatcher dispatcher)
    {
        if (action.Status != ConnectorStatus.Connected)
            return Task.CompletedTask;

        if (action.Connector == Connector.LastFm)
        {
            dispatcher.Dispatch(new FetchRecentPlayHistoryRequest());
            dispatcher.Dispatch(new FetchPlayHistoryAlbumsRequest(HistoryPeriod.Week));
            dispatcher.Dispatch(new FetchPlayHistoryArtistsRequest(HistoryPeriod.Week));
            dispatcher.Dispatch(new FetchPlayHistoryTracksRequest(HistoryPeriod.Week));
        }
        else if (action.Connector == Connector.Database)
        {
            dispatcher.Dispatch(new SearchItemsUpdateRequest());
        }

        return Task.CompletedTask;
    }
}
