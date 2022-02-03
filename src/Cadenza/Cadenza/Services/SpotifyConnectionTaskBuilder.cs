using Cadenza.Common;
using Cadenza.Utilities;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace Cadenza
{
    public class SpotifyConnectionTaskBuilder : IConnectionTaskBuilder
    {
        private readonly IHttpHelper _http;
        private readonly IStoreGetter _storeGetter;
        private readonly IStoreSetter _storeSetter;
        private readonly IConfiguration _config;
        private readonly IJSRuntime _jsRuntime;
        private readonly IConnectorController _connectorController;
        private readonly API.Core.Spotify.IAuthoriser _authoriser;

        private string RedirectUri => _config.GetSection("Spotify").GetValue<string>("RedirectUri");

        public SpotifyConnectionTaskBuilder(IStoreGetter storeGetter, IStoreSetter storeSetter, IConfiguration config,
            IJSRuntime jsRuntime, IConnectorController connectorController, API.Core.Spotify.IAuthoriser lastFmAuthoriser, IHttpHelper http)
        {
            _storeGetter = storeGetter;
            _storeSetter = storeSetter;
            _config = config;
            _jsRuntime = jsRuntime;
            _connectorController = connectorController;
            _authoriser = lastFmAuthoriser;
            _http = http;
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
                "Creating session",
                () => GetAuthUrl(),
                (code) => CreateSession(code),
                ("Authenticating", (url) => Authorise(url)));

            return subTask;
        }

        public async Task NavigateToNewTab(string url)
        {
            await _jsRuntime.InvokeVoidAsync("open", url, "_blank");
        }

        private async Task<bool> IsTaskNeeded()
        {
            var sessionKey = await _storeGetter.GetValue<string>(StoreKey.SpotifyAccessToken);
            return sessionKey == null;
        }

        private async Task<string> Authorise(string authUrl)
        {
            await NavigateToNewTab(authUrl);

            var code = await _storeGetter.AwaitValue<string>(StoreKey.SpotifyCode, 60);

            if (code == null)
                throw new Exception("No code received - need to authenticate on Spotify website");

            return code.Value;
        }

        private async Task CreateSession(string code)
        {
            var authHeader = await _authoriser.GetAuthHeader();

            var requestData = new Dictionary<string, string>
            {
                { "code", code },
                { "redirect_uri", RedirectUri },
                { "grant_type", "authorization_code" }
            };

            var tokenUrl = await _authoriser.GetTokenUrl();

            var response = await _http.Post(tokenUrl, requestData, authHeader);

            var tokens = await response.Content.ReadFromJsonAsync<SpotifyTokens>();

            await _storeSetter.Clear(StoreKey.SpotifyCode);
            await _storeSetter.SetValue(StoreKey.SpotifyAccessToken, tokens.access_token);
            await _storeSetter.SetValue(StoreKey.SpotifyRefreshToken, tokens.refresh_token);
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

        private async Task<string> GetAuthUrl()
        {
            return await _authoriser.GetAuthUrl(RedirectUri);
        }

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

        private class SpotifyTokens
        {
            public string access_token { get; set; }
            public string refresh_token { get; set; }
        }

        private class TokenRequestData
        {
            public string code { get; set; }
            public string redirect_uri { get; set; }
            public string grant_type { get; set; }
        }
    }
}
