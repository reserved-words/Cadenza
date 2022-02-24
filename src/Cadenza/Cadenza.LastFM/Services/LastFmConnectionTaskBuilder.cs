using Cadenza.Core.Common;
using Cadenza.Core.App;
using Cadenza.Core.Interfaces;
using Cadenza.Core.Tasks;
using Cadenza.LastFM;
using Microsoft.JSInterop;
using Microsoft.Extensions.Options;

namespace Cadenza
{
    internal class LastFmConnectionTaskBuilder : IConnectionTaskBuilder
    {
        private readonly IStoreGetter _storeGetter;
        private readonly IStoreSetter _storeSetter;
        private readonly LastFmApiSettings _settings;
        private readonly IJSRuntime _jsRuntime;
        private readonly IConnectorController _connectorController;
        private readonly IAuthoriser _authoriser;

        public LastFmConnectionTaskBuilder(IStoreGetter storeGetter, IStoreSetter storeSetter, IOptions<LastFmApiSettings> settings,
            IJSRuntime jsRuntime, IConnectorController connectorController, IAuthoriser lastFmAuthoriser)
        {
            _storeGetter = storeGetter;
            _storeSetter = storeSetter;
            _settings = settings.Value;
            _jsRuntime = jsRuntime;
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

        public async Task NavigateToNewTab(string url)
        {
            await _jsRuntime.InvokeVoidAsync("open", url, "_blank");
        }

        private async Task<bool> IsTaskNeeded()
        {
            var sessionKey = await _storeGetter.GetValue<string>(StoreKey.LastFmSessionKey);
            return sessionKey == null;
        }

        private async Task<string> GetAuthUrl()
        {
            return await _authoriser.GetAuthUrl(_settings.RedirectUri);
        }

        private async Task<string> Authorise(string authUrl, CancellationToken cancellationToken)
        {
            await NavigateToNewTab(authUrl);

            var token = await _storeGetter.AwaitValue<string>(StoreKey.LastFmToken, 60, cancellationToken);

            if (token == null)
                throw new Exception("No token received - need to authenticate on Last.FM website");

            return token.Value;
        }

        public async Task CreateSession(string token)
        {
            var sessionKey = await _authoriser.CreateSession(token);
            await _storeSetter.SetValue(StoreKey.LastFmSessionKey, sessionKey);
            await _storeSetter.Clear(StoreKey.LastFmToken);
        }
    }
}
