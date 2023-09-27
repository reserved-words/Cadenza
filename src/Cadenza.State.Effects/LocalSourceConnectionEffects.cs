using Cadenza.Web.Source.Local.Interfaces;
using Cadenza.Web.Source.Local.Settings;
using Microsoft.Extensions.Options;

namespace Cadenza.State.Effects;

public class LocalSourceConnectionEffects
{
    private readonly ILocalHttpHelper _httpHelper;
    private readonly IOptions<LocalApiSettings> _apiSettings;
    private readonly IState<LocalSourceConnectionState> _state;

    public LocalSourceConnectionEffects(ILocalHttpHelper httpHelper, IOptions<LocalApiSettings> apiSettings, IState<LocalSourceConnectionState> state)
    {
        _httpHelper = httpHelper;
        _apiSettings = apiSettings;
        _state = state;
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
        catch (Exception) // TODO: Error handling
        {
            dispatcher.Dispatch(new LocalSourceConnectionErroredAction());
        }
    }

    [EffectMethod(typeof(LocalSourceConnectedAction))]
    public Task HandleLocalSourceConnectedAction(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        return Task.CompletedTask;
    }

    private void DispatchProgressAction(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ApplicationStartupProgressAction(Connector.Local, _state.Value.State, _state.Value.Message));
    }
}
