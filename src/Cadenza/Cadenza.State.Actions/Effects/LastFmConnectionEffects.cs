using Cadenza.Web.LastFM.Interfaces;
using Cadenza.Web.LastFM.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IStore = Cadenza.Web.Common.Interfaces.Store.IStore;

namespace Cadenza.State.Actions.Effects;

public class LastFmConnectionEffects
{
    private const string LastFmSessionKey = "LastFmSessionKey";

    private readonly IState<LastFmConnectionState> _state;
    private readonly IStore _store;
    private readonly IAuthoriser _authoriser;
    private readonly IOptions<LastFmApiSettings> _settings;
    private readonly INavigation _navigation;
    private readonly ILogger<LastFmConnectionEffects> _logger;

    public LastFmConnectionEffects(IStore store, IAuthoriser authoriser, IOptions<LastFmApiSettings> settings, INavigation navigation, IState<LastFmConnectionState> state, ILogger<LastFmConnectionEffects> logger)
    {
        _store = store;
        _authoriser = authoriser;
        _settings = settings;
        _navigation = navigation;
        _state = state;
        _logger = logger;
    }

    [EffectMethod(typeof(LastFmConnectRequest))]
    public async Task HandleLastFmConnectRequest(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        var sessionKey = await _store.GetValue(LastFmSessionKey);
        if (!string.IsNullOrEmpty(sessionKey))
        {
            dispatcher.Dispatch(new LastFmFetchSessionKeyResult(sessionKey, false));
        }
        else
        {
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
            await _navigation.NavigateTo(authUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch Last.FM token");
            dispatcher.Dispatch(new LastFmConnectionFailedAction());
        }
    }

    [EffectMethod]
    public Task HandleLastFmFetchTokenResult(LastFmFetchTokenResult action, IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        dispatcher.Dispatch(new LastFmFetchSessionKeyRequest(action.Token));
        return Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleLastFmFetchSessionKeyRequest(LastFmFetchSessionKeyRequest action, IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        try
        {
            var sessionKey = await _authoriser.CreateSession(action.Token);
            dispatcher.Dispatch(new LastFmFetchSessionKeyResult(sessionKey, true));
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
        await _store.SetValue(LastFmSessionKey, action.SessionKey);
        if (action.Reload)
        {
            await _navigation.NavigateTo("/");
        }
        else
        {
            dispatcher.Dispatch(new LastFmConnectedAction());
        }
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
