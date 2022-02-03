using Cadenza.API.Wrapper.Spotify;
using Cadenza.Common;
using Cadenza.Utilities;
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

        private string RedirectUri => _config.GetSection("Spotify").GetValue<string>("RedirectUri");

        public SpotifyConnectionTaskBuilder(IStoreGetter storeGetter, IStoreSetter storeSetter, IConfiguration config,
            IJSRuntime jsRuntime, IConnectorController connectorController, IAuthoriser lastFmAuthoriser, IHttpHelper http)
        {
            _storeGetter = storeGetter;
            _storeSetter = storeSetter;
            _config = config;
            _jsRuntime = jsRuntime;
            _connectorController = connectorController;
            _authoriser = lastFmAuthoriser;
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
                "Refreshing session",
                () => GetAuthUrl(),
                (at) => RefreshSession(at),
                ("Authenticating", (url) => Authorise(url)),
                ("Creating session", (code) => CreateSession(code)));

            return subTask;
        }

        private async Task<bool> IsTaskNeeded()
        {
            var sessionKey = await _storeGetter.GetValue<string>(StoreKey.SpotifyAccessToken);
            return sessionKey == null;
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

        private async Task RefreshSession(string refreshToken)
        {
            var tokens = await _authoriser.RefreshSession(refreshToken);
            await _storeSetter.SetValue(StoreKey.SpotifyAccessToken, tokens.access_token);
            await _storeSetter.SetValue(StoreKey.SpotifyRefreshToken, tokens.refresh_token);
        }

        private async Task NavigateToNewTab(string url)
        {
            await _jsRuntime.InvokeVoidAsync("open", url, "_blank");
        }

        //private async Task InitialisePlayer(string accessToken)
        //{
        //    await _startup.InitialisePlayer(accessToken);
        //}

        //public async Task StartSession(string code, string redirectUri)
        //{
        //    await RefreshSpotify(tokens.refresh_token);
        //}

        //public async Task<string> GetAccessToken()
        //{
        //    return (await _storeGetter.GetValue<string>(StoreKey.SpotifyAccessToken)).Value;
        //}

        //public async Task<bool> InitialisePlayer(string accessToken)
        //{
        //    return await _interop.ConnectPlayer(accessToken);
        //}

        //private async Task RefreshSpotify(string refreshToken)
        //{
        //    var requestData = new Dictionary<string, string>
        //{
        //    { "grant_type", "refresh_token" },
        //    { "refresh_token", refreshToken }
        //};

        //    var tokenUrl = await _authoriser.GetTokenUrl();
        //    var authHeader = await _authoriser.GetAuthHeader();

        //    var response = await _http.Post(tokenUrl, requestData, authHeader);

        //    var tokens = await response.Content.ReadFromJsonAsync<SpotifyTokens>();
        //    await SaveSpotifyTokens(tokens);
        //}

    }
}
