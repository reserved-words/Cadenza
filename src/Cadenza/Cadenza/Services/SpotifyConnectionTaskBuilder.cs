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
        private readonly IInitialiser _initialiser;

        private string RedirectUri => _config.GetSection("Spotify").GetValue<string>("RedirectUri");

        public SpotifyConnectionTaskBuilder(IStoreGetter storeGetter, IStoreSetter storeSetter, IConfiguration config,
            IJSRuntime jsRuntime, IConnectorController connectorController, IAuthoriser lastFmAuthoriser, IInitialiser initialiser)
        {
            _storeGetter = storeGetter;
            _storeSetter = storeSetter;
            _config = config;
            _jsRuntime = jsRuntime;
            _connectorController = connectorController;
            _authoriser = lastFmAuthoriser;
            _initialiser = initialiser;
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
                 (ct) => GetAuthUrl(),
                 (at, ct) => RefreshSession(at),
                 ("Authenticating", (url, ct) => Authorise(url, ct)),
                 ("Creating session", (code, ct) => CreateSession(code)));

            subTask.AddStep("Populating library", Populate);

            return subTask; 

        }

        private async Task<bool> IsTaskNeeded()
        {
            var accessToken = await _storeGetter.GetValue<string>(StoreKey.SpotifyAccessToken);

            if (accessToken != null && accessToken.Updated > DateTime.Now.AddHours(-1))
            {
                await Populate();
                return false;
            }

            await _storeSetter.Clear(StoreKey.SpotifyAccessToken);
            await _storeSetter.Clear(StoreKey.SpotifyCode);
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

        private async Task<string> Authorise(string authUrl, CancellationToken cancellationToken)
        {
            await NavigateToNewTab(authUrl);

            var code = await _storeGetter.AwaitValue<string>(StoreKey.SpotifyCode, 60, cancellationToken);

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
            return tokens.access_token;
        }

        private async Task NavigateToNewTab(string url)
        {
            await _jsRuntime.InvokeVoidAsync("open", url, "_blank");
        }

        private async Task Populate()
        {
            try
            {
                var accessToken = await _storeGetter.GetValue<string>(StoreKey.SpotifyAccessToken);

                var httpClient = new HttpClient();
                var http = new HttpHelper(httpClient);
                var url = $"http://localhost:5141/Spotify/Library/Populate?accessToken={accessToken.Value}";
                var response = await http.Post(url, null, null);

                //await _initialiser.Populate(accessToken.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to populate Spotify library");
            }
        }
    }
}
