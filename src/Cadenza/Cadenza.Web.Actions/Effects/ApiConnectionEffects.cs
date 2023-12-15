using Microsoft.Extensions.Logging;

namespace Cadenza.Web.Actions.Effects;

public class ApiConnectionEffects
{
    private readonly IStartupApi _api;
    private readonly IState<ApiConnectionState> _state;
    private readonly ILogger<ApiConnectionEffects> _logger;

    public ApiConnectionEffects(IState<ApiConnectionState> state, ILogger<ApiConnectionEffects> logger, IStartupApi api)
    {
        _state = state;
        _logger = logger;
        _api = api;
    }

    [EffectMethod(typeof(ApiConnectRequest))]
    public async Task HandleApiConnectRequest(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        try
        {
            await _api.Connect();
            dispatcher.Dispatch(new ApiConnectedAction());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to API");
            dispatcher.Dispatch(new ApiConnectionFailedAction());
        }
    }

    [EffectMethod(typeof(ApiConnectionFailedAction))]
    public Task HandleApiConnectionFailedAction(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        return Task.CompletedTask;
    }

    [EffectMethod(typeof(ApiConnectedAction))]
    public Task HandleApiConnectedAction(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        dispatcher.Dispatch(new SearchItemsUpdateRequest());
        dispatcher.Dispatch(new FetchGroupingsRequest());
        dispatcher.Dispatch(new FetchRecentlyPlayedAlbumsRequest());
        dispatcher.Dispatch(new FetchRecentlyPlayedTracksRequest());
        dispatcher.Dispatch(new FetchRecentlyAddedAlbumsRequest());
        dispatcher.Dispatch(new FetchTopPlayedAlbumsRequest(HistoryPeriod.Week));
        dispatcher.Dispatch(new FetchTopPlayedArtistsRequest(HistoryPeriod.Week));
        return Task.CompletedTask;
    }

    private void DispatchProgressAction(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ApplicationStartupProgressAction(ConnectionType.Api, _state.Value.State, _state.Value.Message));
    }
}
