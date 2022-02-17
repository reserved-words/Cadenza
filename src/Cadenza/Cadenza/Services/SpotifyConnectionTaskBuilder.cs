using Cadenza.API.Wrapper.Spotify;
using Cadenza.Common;
using Cadenza.Source.Spotify.Player;
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
        private readonly ISpotifyInterop _interop;
        private readonly IInitialiser _initialiser;
        private readonly IPlayerApi _player;

        private string RedirectUri => _config.GetSection("Spotify").GetValue<string>("RedirectUri");

        public SpotifyConnectionTaskBuilder(IStoreGetter storeGetter, IStoreSetter storeSetter, IConfiguration config,
            IJSRuntime jsRuntime, IConnectorController connectorController, IAuthoriser lastFmAuthoriser, ISpotifyInterop interop, IInitialiser initialiser, IPlayerApi player)
        {
            _storeGetter = storeGetter;
            _storeSetter = storeSetter;
            _config = config;
            _jsRuntime = jsRuntime;
            _connectorController = connectorController;
            _authoriser = lastFmAuthoriser;
            _interop = interop;
            _initialiser = initialiser;
            _player = player;
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
                 (ct) => GetAuthUrl(),
                 (at, ct) => InitialisePlayer(at, ct),
                 ("Authenticating", (url, ct) => Authorise(url, ct)),
                 ("Creating session", (code, ct) => CreateSession(code)),
                 ("Refreshing session", (at, ct) => RefreshSession(at)));

            subTask.AddStep("Populating library", Populate);

            return subTask; 

        }

        private async Task<bool> IsTaskNeeded()
        {
            var accessToken = await _storeGetter.GetValue<string>(StoreKey.SpotifyAccessToken);

            if (accessToken != null && accessToken.Updated > DateTime.Now.AddHours(-1))
            {
                var devices = await _player.GetDevices();
                var device = devices?.Devices.SingleOrDefault(d => d.name == "Cadenza");
                if (device != null)
                {
                    await _storeSetter.SetValue(StoreKey.SpotifyDeviceId, device.id);
                    await Populate();
                    return false;
                }
            }

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

        public async Task InitialisePlayer(string accessToken, CancellationToken cancellationToken)
        {
            return;

            //var connected = await _interop.ConnectPlayer(accessToken);
            //if (!connected)
            //    throw new Exception("Failed to connect to Spotify player");

            //var deviceId = await _storeGetter.AwaitValue<string>(StoreKey.SpotifyDeviceId, 60, cancellationToken);

            //if (deviceId == null)
            //    throw new Exception("No Device ID received - Spotify player not ready");
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
