using Cadenza.Web.Common.Interfaces.Store;
using Cadenza.Web.LastFM.Interfaces;
using Cadenza.Web.LastFM.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cadenza.State.Effects;

public class LastFmConnectionEffects
{
    private readonly IState<LastFmConnectionState> _state;
    private readonly IAppStore _store;
    private readonly IAuthoriser _authoriser;
    private readonly IOptions<LastFmApiSettings> _settings;
    private readonly INavigation _navigation;
    private readonly ILogger<LastFmConnectionEffects> _logger;

    public LastFmConnectionEffects(IAppStore store, IAuthoriser authoriser, IOptions<LastFmApiSettings> settings, INavigation navigation, IState<LastFmConnectionState> state, ILogger<LastFmConnectionEffects> logger)
    {
        _store = store;
        _authoriser = authoriser;
        _settings = settings;
        _navigation = navigation;
        _state = state;
        _logger = logger;
    }

    // TODO: Handle expired session key / token - both if know expired and if error due to expired?

    [EffectMethod(typeof(LastFmConnectRequest))]
    public async Task HandleLastFmConnectRequest(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        var sessionKey = await _store.GetValue<string>(StoreKey.LastFmSessionKey);
        if (sessionKey != null && !string.IsNullOrEmpty(sessionKey.Value) && !sessionKey.IsExpired)
        {
            dispatcher.Dispatch(new LastFmFetchSessionKeyResult(sessionKey.Value));
        }
        else
        {
            await _store.Clear(StoreKey.LastFmSessionKey);
            await _store.Clear(StoreKey.LastFmToken);
            dispatcher.Dispatch(new LastFmFetchTokenRequest());
        }
    }

    [EffectMethod(typeof(LastFmFetchTokenRequest))]
    public async Task HandleLastFmFetchTokenRequest(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        try
        {
            var authUrl = await _authoriser.GetAuthUrl(_settings.Value.RedirectUri);
            await _navigation.OpenNewTab(authUrl);
            dispatcher.Dispatch(new LastFmFetchSessionKeyRequest());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch Last.FM token");
            dispatcher.Dispatch(new LastFmConnectionFailedAction());
        }
    }

    [EffectMethod]
    public async Task HandleLastFmFetchTokenResult(LastFmFetchTokenResult action, IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        await Task.Delay(5000);
        await _store.SetValue(StoreKey.LastFmToken, action.Token);
        // No point dispatching any actions here if this happens in a new tab?
        // Could do in same tab - any reason not to?
        // If so could then dispatch action here instead
    }

    [EffectMethod(typeof(LastFmFetchSessionKeyRequest))]
    public async Task HandleLastFmFetchSessionKeyRequest(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        try
        {
            var token = await _store.AwaitValue<string>(StoreKey.LastFmToken, 60);

            if (token == null)
                throw new Exception("No token received - need to authenticate on Last.FM website");

            var sessionKey = await _authoriser.CreateSession(token.Value);

            dispatcher.Dispatch(new LastFmFetchSessionKeyResult(sessionKey));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch session key");
            dispatcher.Dispatch(new LastFmConnectionFailedAction());
        }
    }

    [EffectMethod]
    public async Task HandleLastFmFetchSessionKeyResult(LastFmFetchSessionKeyResult action, IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        await _store.Clear(StoreKey.LastFmToken);
        await _store.SetValue(StoreKey.LastFmSessionKey, action.SessionKey);
        dispatcher.Dispatch(new LastFmConnectedAction());
    }

    [EffectMethod(typeof(LastFmConnectionFailedAction))]
    public Task HandleLastFmConnectionFailedAction(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        return Task.CompletedTask;
    }

    [EffectMethod(typeof(LastFmConnectedAction))]
    public Task HandleLastFmConnectedAction(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        dispatcher.Dispatch(new FetchRecentPlayHistoryRequest());
        dispatcher.Dispatch(new FetchPlayHistoryAlbumsRequest(HistoryPeriod.Week));
        dispatcher.Dispatch(new FetchPlayHistoryArtistsRequest(HistoryPeriod.Week));
        dispatcher.Dispatch(new FetchPlayHistoryTracksRequest(HistoryPeriod.Week));
        return Task.CompletedTask;
    }

    private void DispatchProgressAction(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ApplicationStartupProgressAction(ConnectionType.LastFm, _state.Value.State, _state.Value.Message));
    }
}
