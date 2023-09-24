﻿using Cadenza.Web.Common.Events;
using Cadenza.Web.Common.Tasks;
using Cadenza.Web.Source.Local.Interfaces;
using Cadenza.Web.Source.Local.Settings;
using Microsoft.Extensions.Options;

namespace Cadenza.State.Effects;

public class LocalSourceConnectionEffects
{
    private readonly ILocalHttpHelper _httpHelper;
    private readonly IOptions<LocalApiSettings> _apiSettings;

    public LocalSourceConnectionEffects(ILocalHttpHelper httpHelper, IOptions<LocalApiSettings> apiSettings)
    {
        _httpHelper = httpHelper;
        _apiSettings = apiSettings;
    }

    [EffectMethod(typeof(LocalSourceConnectRequest))]
    public async Task HandleLocalSourceConnectRequest(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new StartupTaskProgressAction(Connector.Local, "Connecting", TaskState.Running));
        try
        {
            await _httpHelper.Get(_apiSettings.Value.Endpoints.Connect);
            dispatcher.Dispatch(new LocalSourceConnectedAction());
        }
        catch (Exception) // TODO: Error handling
        {
            dispatcher.Dispatch(new StartupTaskProgressAction(Connector.Local, "Failed to connect", TaskState.Errored));
        }
    }

    [EffectMethod(typeof(LocalSourceConnectedAction))]
    public Task HandleLocalSourceConnectedAction(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new StartupTaskProgressAction(Connector.Local, "Connection succeedeed", TaskState.Completed));
        return Task.CompletedTask;
    }
}
