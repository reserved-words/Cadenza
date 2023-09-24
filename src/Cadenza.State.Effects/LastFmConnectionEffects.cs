﻿using Cadenza.Web.Common.Events;
using Cadenza.Web.Common.Interfaces.Store;
using Cadenza.Web.Common.Tasks;
using Cadenza.Web.LastFM.Interfaces;
using Cadenza.Web.LastFM.Settings;
using Microsoft.Extensions.Options;

namespace Cadenza.State.Effects;

public class LastFmConnectionEffects
{
    private readonly IAppStore _store;
    private readonly IAuthoriser _authoriser;
    private readonly IOptions<LastFmApiSettings> _settings;
    private readonly INavigation _navigation;

    public LastFmConnectionEffects(IAppStore store, IAuthoriser authoriser, IOptions<LastFmApiSettings> settings, INavigation navigation)
    {
        _store = store;
        _authoriser = authoriser;
        _settings = settings;
        _navigation = navigation;
    }

    // TODO: Error handling
    // TODO: Handle expired session key / token - both if know expired and if error due to expired?

    [EffectMethod(typeof(LastFmConnectRequest))]
    public async Task HandleLastFmConnectRequest(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new StartupTaskProgressAction(Connector.LastFm, "Checking if session key already exists", TaskState.Running));
        var sessionKey = await _store.GetValue<string>(StoreKey.LastFmSessionKey);
        if (sessionKey != null && !string.IsNullOrEmpty(sessionKey.Value) && !sessionKey.IsExpired)
        {
            dispatcher.Dispatch(new LastFmConnectedAction());
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
        dispatcher.Dispatch(new StartupTaskProgressAction(Connector.LastFm, "Fetching token URL", TaskState.Running));
        var authUrl = await _authoriser.GetAuthUrl(_settings.Value.RedirectUri);
        dispatcher.Dispatch(new StartupTaskProgressAction(Connector.LastFm, "Navigating to token URL", TaskState.Running));
        await _navigation.OpenNewTab(authUrl);
        dispatcher.Dispatch(new LastFmFetchSessionKeyRequest());
    }

    [EffectMethod]
    public async Task HandleLastFmFetchTokenResult(LastFmFetchTokenResult action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new StartupTaskProgressAction(Connector.LastFm, "Saving token", TaskState.Running));
        await _store.SetValue(StoreKey.LastFmToken, action.Token);
        // No point dispatching any actions here if this happens in a new tab?
        // Could do in same tab - any reason not to?
        // If so could then dispatch action here instead
    }

    [EffectMethod(typeof(LastFmFetchSessionKeyRequest))]
    public async Task HandleLastFmFetchSessionKeyRequest(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new StartupTaskProgressAction(Connector.LastFm, "Awaiting token", TaskState.Running));
        var token = await _store.AwaitValue<string>(StoreKey.LastFmToken, 60);

        if (token == null)
            throw new Exception("No token received - need to authenticate on Last.FM website");

        await _store.Clear(StoreKey.LastFmToken);

        dispatcher.Dispatch(new StartupTaskProgressAction(Connector.LastFm, "Fetching session key", TaskState.Running));
        var sessionKey = await _authoriser.CreateSession(token.Value);

        dispatcher.Dispatch(new LastFmFetchSessionKeyResult(sessionKey));
    }

    [EffectMethod]
    public async Task HandleLastFmFetchSessionKeyResult(LastFmFetchSessionKeyResult action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new StartupTaskProgressAction(Connector.LastFm, "Saving session key", TaskState.Running));
        await _store.SetValue(StoreKey.LastFmSessionKey, action.SessionKey);
        dispatcher.Dispatch(new LastFmConnectedAction());
    }

    [EffectMethod(typeof(LastFmConnectedAction))]
    public Task HandleLastFmConnectedAction(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new StartupTaskProgressAction(Connector.LastFm, "Fetching Last.FM data", TaskState.Running));
        dispatcher.Dispatch(new FetchRecentPlayHistoryRequest());
        dispatcher.Dispatch(new FetchPlayHistoryAlbumsRequest(HistoryPeriod.Week));
        dispatcher.Dispatch(new FetchPlayHistoryArtistsRequest(HistoryPeriod.Week));
        dispatcher.Dispatch(new FetchPlayHistoryTracksRequest(HistoryPeriod.Week));
        dispatcher.Dispatch(new StartupTaskProgressAction(Connector.LastFm, "Connection succeeded", TaskState.Completed));
        return Task.CompletedTask;
    }
}
