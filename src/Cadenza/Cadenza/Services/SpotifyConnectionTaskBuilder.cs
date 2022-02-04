using Cadenza.API.Wrapper.Spotify;
using Cadenza.Common;
using Cadenza.Source.Spotify;
using Microsoft.JSInterop;

namespace Cadenza
{
    public class SpotifyConnectionTaskBuilder : IConnectionTaskBuilder
    {
        private readonly IStoreGetter _storeGetter;
        private readonly IStoreSetter _storeSetter;
        private readonly IConfiguration _config;
        private readonly IJSRuntime _jsRuntime;
        private readonly IConnectorController _connectorController;
        private readonly IAuthoriser _authoriser;
        private readonly ISpotifyInterop _interop;

        private string RedirectUri => _config.GetSection("Spotify").GetValue<string>("RedirectUri");

        public SpotifyConnectionTaskBuilder(IStoreGetter storeGetter, IStoreSetter storeSetter, IConfiguration config,
            IJSRuntime jsRuntime, IConnectorController connectorController, IAuthoriser lastFmAuthoriser, ISpotifyInterop interop)
        {
            _storeGetter = storeGetter;
            _storeSetter = storeSetter;
            _config = config;
            _jsRuntime = jsRuntime;
            _connectorController = connectorController;
            _authoriser = lastFmAuthoriser;
            _interop = interop;
        }

        public SubTask GetConnectionTask()
        {
            var subTask = new SubTask
            {
                Id = "Spotify",
                Title = "Connect to Spotify",
                CheckStep = new TaskCheckStep { Caption = "Checking if Spotify already connected", Task = IsTaskNeeded },
                Steps = new List<TaskStep>(),
                OnError = (ex) => _connectorController.SetStatus(Connector.Spotify, ConnectorStatus.Errored, ex.Message),
                OnCompleted = () => _connectorController.SetStatus(Connector.Spotify, ConnectorStatus.Connected)
            };

            subTask.AddSteps(
                 "Getting auth URL",
                 "Initialising player",
                 () => GetAuthUrl(),
                 (at) => InitialisePlayer(at),
                 ("Authenticating", (url) => Authorise(url)),
                 ("Creating session", (code) => CreateSession(code)),
                 ("Refreshing session", (at) => RefreshSession(at)));

            return subTask; 

        }

        private async Task<bool> IsTaskNeeded()
        {
            await _storeSetter.Clear(StoreKey.SpotifyAccessToken);
            await _storeSetter.Clear(StoreKey.SpotifyCode);
            await _storeSetter.Clear(StoreKey.SpotifyDeviceId);
            await _storeSetter.Clear(StoreKey.SpotifyState);
            await _storeSetter.Clear(StoreKey.SpotifyRefreshToken);
            return true;
        }

        private async Task<string> GetAuthUrl()
        {
            var guid = Guid.NewGuid();
            var state = guid.ToString().Substring(0, 16);
            await _storeSetter.SetValue(StoreKey.SpotifyState, state);
            return await _authoriser.GetAuthUrl(state, RedirectUri);
        }

        private async Task<string> Authorise(string authUrl)
        {
            await NavigateToNewTab(authUrl);

            var code = await _storeGetter.AwaitValue<string>(StoreKey.SpotifyCode, 60);

            if (code == null)
                throw new Exception("No code received - need to authenticate on Spotify website");

            return code.Value;
        }

        private async Task<string> CreateSession(string code)
        {
            var tokens = await _authoriser.CreateSession(code, RedirectUri);
            await _storeSetter.Clear(StoreKey.SpotifyCode);
            await _storeSetter.SetValue(StoreKey.SpotifyAccessToken, tokens.access_token);
            await _storeSetter.SetValue(StoreKey.SpotifyRefreshToken, tokens.refresh_token);
            return tokens.refresh_token;
        }

        private async Task<string> RefreshSession(string refreshToken)
        {
            var tokens = await _authoriser.RefreshSession(refreshToken);
            await _storeSetter.SetValue(StoreKey.SpotifyAccessToken, tokens.access_token);
            await _storeSetter.SetValue(StoreKey.SpotifyRefreshToken, tokens.refresh_token);
            return tokens.refresh_token;
        }

        private async Task NavigateToNewTab(string url)
        {
            await _jsRuntime.InvokeVoidAsync("open", url, "_blank");
        }

        public async Task InitialisePlayer(string accessToken)
        {
            //await _interop.ConnectPlayer(accessToken);
            var deviceId = await _storeGetter.AwaitValue<string>(StoreKey.SpotifyDeviceId, 60);

            if (deviceId == null)
                throw new Exception("No Device ID received - Spotify player not ready");


        }
    }
}
