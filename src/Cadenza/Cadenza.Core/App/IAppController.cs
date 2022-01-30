namespace Cadenza.Core;

public interface IAppController
{
    void Initialise();

    Task Pause();
    Task Resume();
    Task SkipNext();
    Task SkipPrevious();

    Task LoadingPlaylist();

    Task EnableConnector(Connector connector);
    Task DisableConnector(Connector connector, ConnectorError error, string message);

    Task Play(PlaylistDefinition playlistDefinition);
}
