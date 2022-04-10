using Cadenza.Core.Common;
using Cadenza.Core.App;
using Cadenza.Core.Interfaces;
using Cadenza.Core.Tasks;
using Cadenza.Source.Spotify.Interfaces;
using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Library;
using Cadenza.Utilities;
using Microsoft.Extensions.Configuration;
using Cadenza.Domain;

namespace Cadenza.Source.Spotify.Services;

internal class SpotifyConnectionTaskBuilder : IConnectionTaskBuilder
{
    private readonly IApiToken _apiToken;
    private readonly IStoreGetter _storeGetter;
    private readonly IConnectorController _connectorController;
    private readonly ISpotifyAuthHelper _authHelper;
    private readonly ILibrary _apiLibrary;
    private readonly IHttpHelper _httpHelper;
    private readonly IConfiguration _config;

    public SpotifyConnectionTaskBuilder(IStoreGetter storeGetter,
        IConnectorController connectorController, ISpotifyAuthHelper authHelper, IApiToken apiToken, ILibrary apiLibrary, IHttpHelper httpHelper, IConfiguration config)
    {
        _storeGetter = storeGetter;
        _connectorController = connectorController;
        _authHelper = authHelper;
        _apiToken = apiToken;
        _apiLibrary = apiLibrary;
        _httpHelper = httpHelper;
        _config = config;
    }

    public SubTask GetConnectionTask()
    {
        var subTask = new SubTask
        {
            Id = "Spotify",
            Title = "Connect to Spotify",
            Steps = new List<TaskStep>(),
            OnError = (ex) => _connectorController.SetStatus(Connector.Spotify, ConnectorStatus.Errored, ex.Message),
            OnCompleted = () => _connectorController.SetStatus(Connector.Spotify, ConnectorStatus.Connected)
        };

        subTask.AddStep("Creating session", _authHelper.GetAccessToken);

        subTask.AddStep("Populating library", Populate);

        return subTask;
    }

    private async Task Populate()
    {
        var accessToken = await _storeGetter.GetValue<string>(StoreKey.SpotifyAccessToken);
        _apiToken.SetAccessToken(accessToken.Value);
        var fullLibrary = await _apiLibrary.Get();
        var baseUrl = _config.GetValue<string>("LocalApi:BaseUrl");
        var endpoint = _config.GetValue<string>("LocalApi:Endpoints:AddSource");
        var url = $"{baseUrl}{endpoint}";
        var sourceLibrary = new ExternalSourceLibrary
        {
            Source = LibrarySource.Spotify,
            Library = fullLibrary
        };
        await _httpHelper.Post(url, null, sourceLibrary);
    }
}
