using Cadenza.Web.Common.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace Cadenza.Web.Actions.Effects;

public class LastFmConnectionEffects
{
    private readonly IState<LastFmConnectionState> _state;
    private readonly ILastFmApi _api;
    private readonly IOptions<AppSettings> _settings;
    private readonly ILogger<LastFmConnectionEffects> _logger;

    public LastFmConnectionEffects(ILastFmApi api, IOptions<AppSettings> settings, IState<LastFmConnectionState> state, ILogger<LastFmConnectionEffects> logger)
    {
        _api = api;
        _settings = settings;
        _state = state;
        _logger = logger;
    }

    [EffectMethod(typeof(LastFmConnectRequest))]
    public async Task HandleLastFmConnectRequest(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        var doesSessionExist = await _api.DoesSessionExist();
        if (doesSessionExist)
        {
            dispatcher.Dispatch(new LastFmConnectedAction());
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
            var authUrl = await _api.GetAuthUrl(_settings.Value.LastFmRedirectUri);
            dispatcher.Dispatch(new NavigationRequest(authUrl, false));
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
        dispatcher.Dispatch(new LastFmCreateSessionRequest(action.Token));
        return Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleLastFmFetchSessionKeyRequest(LastFmCreateSessionRequest action, IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        try
        {
            await _api.CreateSession(action.Token);
            dispatcher.Dispatch(new NavigationRequest("/", false));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch session key");
            dispatcher.Dispatch(new LastFmConnectionFailedAction());
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
        return Task.CompletedTask;
    }

    private void DispatchProgressAction(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ApplicationStartupProgressAction(ConnectionType.LastFm, _state.Value.State, _state.Value.Message));
    }
}
