using Cadenza.Web.Source.Local.Interfaces;
using Cadenza.Web.Source.Local.Settings;
using Cadenza.Web.State.Actions;
using Cadenza.Web.State.Store;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cadenza.Web.Actions.Effects;

public class LocalSourceConnectionEffects
{
    private readonly ILocalHttpHelper _httpHelper;
    private readonly IOptions<LocalApiSettings> _apiSettings;
    private readonly IState<LocalSourceConnectionState> _state;
    private readonly ILogger<LocalSourceConnectionEffects> _logger;

    public LocalSourceConnectionEffects(ILocalHttpHelper httpHelper, IOptions<LocalApiSettings> apiSettings, IState<LocalSourceConnectionState> state, ILogger<LocalSourceConnectionEffects> logger)
    {
        _httpHelper = httpHelper;
        _apiSettings = apiSettings;
        _state = state;
        _logger = logger;
    }

    [EffectMethod(typeof(LocalSourceConnectRequest))]
    public async Task HandleLocalSourceConnectRequest(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        try
        {
            await _httpHelper.Get(_apiSettings.Value.Endpoints.Connect);
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
