using Cadenza.Core.Common;
using Cadenza.Core.App;
using Cadenza.Core.Interfaces;
using Cadenza.Core.Tasks;
using Cadenza.Source.Spotify.Interfaces;

namespace Cadenza.Source.Spotify.Services;

internal class SpotifyConnectionTaskBuilder : IConnectionTaskBuilder
{
    private readonly IStoreGetter _storeGetter;
    private readonly IConnectorController _connectorController;
    private readonly ISpotifyAuthHelper _authHelper;
    private readonly IInitialiser _initialiser;

    public SpotifyConnectionTaskBuilder(IStoreGetter storeGetter,
        IConnectorController connectorController, ISpotifyAuthHelper authHelper, IInitialiser initialiser)
    {
        _storeGetter = storeGetter;
        _connectorController = connectorController;
        _authHelper = authHelper;
        _initialiser = initialiser;
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
        try
        {
            var accessToken = await _storeGetter.GetValue<string>(StoreKey.SpotifyAccessToken);
            await _initialiser.Populate(accessToken.Value);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to populate Spotify library");
        }
    }
}
