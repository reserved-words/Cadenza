using Cadenza.Web.Common.Interfaces.Connections;
using Cadenza.Web.Common.Interfaces.Startup;
using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Web.LastFM.Services;

internal class LastFmConnector : IConnector
{
    private readonly IAppStore _store;
    private readonly IOptions<LastFmApiSettings> _settings;
    private readonly INavigation _navigation;
    private readonly IConnectionCoordinator _connectorController;
    private readonly IAuthoriser _authoriser;

    public LastFmConnector(IAppStore store, IOptions<LastFmApiSettings> settings,
        INavigation navigation, IConnectionCoordinator connectorController, IAuthoriser lastFmAuthoriser)
    {
        _store = store;
        _settings = settings;
        _navigation = navigation;
        _connectorController = connectorController;
        _authoriser = lastFmAuthoriser;
    }

    public SubTask GetConnectionTask()
    {
        // TODO: Handle if session key has been revoked - if so need to start Last.FM connection process again
        // (or for now as long as relevant error message displayed could just clear the session key and user can retry)

        var subTask = new SubTask
        {
            Id = "LastFM",
            Title = "Connect to Last.FM",
            CheckStep = new TaskCheckStep { Caption = "Checking if Last.FM already connected", Task = IsTaskNeeded },
            Steps = new List<TaskStep>(),
            OnError = (ex) => _connectorController.SetStatus(Connector.LastFm, ConnectorStatus.Errored, ex.Message),
            OnCompleted = () => _connectorController.SetStatus(Connector.LastFm, ConnectorStatus.Connected)
        };

        subTask.AddSteps(
            "Getting auth URL",
            "Saving session key",
            (ct) => GetAuthUrl(),
            (sk, ct) => CreateSession(sk),
            ("Authenticating", (url, ct) => Authorise(url, ct)));

        return subTask;
    }

    public async Task CreateSession(string token)
    {
        var sessionKey = await _authoriser.CreateSession(token);
        await _store.SetValue(StoreKey.LastFmSessionKey, sessionKey);
        await _store.Clear(StoreKey.LastFmToken);
    }

    public async Task NavigateToNewTab(string url)
    {
        await _navigation.OpenNewTab(url);
    }

    private async Task<bool> IsTaskNeeded()
    {
        var sessionKey = await _store.GetValue<string>(StoreKey.LastFmSessionKey);
        return sessionKey == null;
    }

    private async Task<string> GetAuthUrl()
    {
        return await _authoriser.GetAuthUrl(_settings.Value.RedirectUri);
    }

    private async Task<string> Authorise(string authUrl, CancellationToken cancellationToken)
    {
        await NavigateToNewTab(authUrl);

        var token = await _store.AwaitValue<string>(StoreKey.LastFmToken, 60, cancellationToken);

        if (token == null)
            throw new Exception("No token received - need to authenticate on Last.FM website");

        return token.Value;
    }
}
