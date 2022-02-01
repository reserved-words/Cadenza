namespace Cadenza.Core;

public interface IAppController
{
    void Initialise();

    Task Pause();
    Task Resume();
    Task SkipNext();
    Task SkipPrevious();

    Task LoadingPlaylist();

    Task Play(PlaylistDefinition playlistDefinition);
}
