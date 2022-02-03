//using Cadenza.Common;
//using Cadenza.Source.Spotify;
//using Microsoft.JSInterop;

//namespace Cadenza
//{
//    public class SpotifyConnectionTaskBuilder : IConnectionTaskBuilder
//    {
//        private readonly IStoreGetter _storeGetter;
//        private readonly IStoreSetter _storeSetter;
//        private readonly IConfiguration _config;
//        private readonly IJSRuntime _jsRuntime;
//        private readonly IConnectorController _connectorController;
//        private readonly API.Core.Spotify.IAuthoriser _authoriser;
//        private readonly ISpotifyStartup _startup;

//        private string RedirectUri => _config.GetSection("Spotify").GetValue<string>("RedirectUri");

//        public SpotifyConnectionTaskBuilder(IStoreGetter storeGetter, IStoreSetter storeSetter, IConfiguration config,
//            IJSRuntime jsRuntime, IConnectorController connectorController, API.Core.Spotify.IAuthoriser lastFmAuthoriser, ISpotifyStartup startup)
//        {
//            _storeGetter = storeGetter;
//            _storeSetter = storeSetter;
//            _config = config;
//            _jsRuntime = jsRuntime;
//            _connectorController = connectorController;
//            _authoriser = lastFmAuthoriser;
//            _startup = startup;
//        }

//        public SubTask GetConnectionTask()
//        {
//            var subTask = new SubTask
//            {
//                Id = "Spotify",
//                Title = "Connect to Spotify",
//                CheckStep = new TaskCheckStep { Caption = "Checking if Spotify already connected", Task = IsTaskNeeded },
//                Steps = new List<TaskStep>(),
//                OnError = (ex) => _connectorController.SetStatus(Connector.Spotify, ConnectorStatus.Errored, ex.Message),
//                OnCompleted = () => _connectorController.SetStatus(Connector.Spotify, ConnectorStatus.Connected)
//            };

//            subTask.AddSteps(
//                "Getting auth URL",
//                "Initialising player",
//                () => GetAuthUrl(),
//                (ac) => InitialisePlayer(ac),
//                ("Authenticating", (url) => Authorise(url)),
//                ("Creating session", (code) => CreateSession(code)));

//            return subTask;
//        }

//        public async Task NavigateToNewTab(string url)
//        {
//            await _jsRuntime.InvokeVoidAsync("open", url, "_blank");
//        }

//        private async Task<bool> IsTaskNeeded()
//        {
//            var sessionKey = await _storeGetter.GetValue<string>(StoreKey.SpotifyAccessToken);
//            return sessionKey == null;
//        }

//        private async Task<string> GetAuthUrl()
//        {
//            return await _startup.GetAuthUrl(RedirectUri);
//        }

//        private async Task<string> Authorise(string authUrl)
//        {
//            await NavigateToNewTab(authUrl);

//            var startTime = DateTime.Now;
//            var endTime = startTime.AddSeconds(60);

//            var code = await _storeGetter.GetValue<string>(StoreKey.SpotifyCode);

//            while (code == null && DateTime.Now < endTime)
//            {
//                await Task.Delay(500);
//                code = await _storeGetter.GetValue<string>(StoreKey.SpotifyCode);
//            }

//            if (code == null)
//                throw new Exception("No code received - need to authenticate on Spotify website");

//            return code.Value;
//        }

//        private async Task<string> CreateSession(string code)
//        {
//            await _startup.StartSession(code, RedirectUri);
//            return (await _storeGetter.GetValue<string>(StoreKey.SpotifyAccessToken)).Value;
//        }

//        private async Task InitialisePlayer(string accessToken)
//        {
//            await _startup.InitialisePlayer(accessToken);
//        }
//    }
//}
