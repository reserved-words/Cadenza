using Cadenza.Common.Domain.Enums;
using Cadenza.State.Actions;
using Cadenza.Web.Common.Enums;
using Cadenza.Web.Common.Interfaces;
using Fluxor;

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
        if (action.Connector == Connector.LastFm && action.Status == ConnectorStatus.Connected)
        {
            dispatcher.Dispatch(new FetchRecentPlayHistoryRequest());
            dispatcher.Dispatch(new FetchPlayHistoryAlbumsRequest(HistoryPeriod.Week));
            dispatcher.Dispatch(new FetchPlayHistoryArtistsRequest(HistoryPeriod.Week));
            dispatcher.Dispatch(new FetchPlayHistoryTracksRequest(HistoryPeriod.Week));
        }
        return Task.CompletedTask;
    }
}
