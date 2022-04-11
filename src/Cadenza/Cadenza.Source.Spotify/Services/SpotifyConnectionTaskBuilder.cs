using Cadenza.Core.Common;
using Cadenza.Core.Interfaces;
using Cadenza.Core.Tasks;
using Cadenza.Source.Spotify.Interfaces;

namespace Cadenza.Source.Spotify.Services;

internal class SpotifyConnectionTaskBuilder : IConnectionTaskBuilder
{
    private readonly IConnectorController _connectorController;
    private readonly ISpotifyAuthHelper _authHelper;

    public SpotifyConnectionTaskBuilder(IConnectorController connectorController, ISpotifyAuthHelper authHelper)
    {
        _connectorController = connectorController;
        _authHelper = authHelper;
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

        return subTask;
    }
}
