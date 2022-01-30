namespace Cadenza.Core;

public interface IAppController
{
    void Initialise();

    Task Pause();
    Task Resume();
    Task SkipNext();
    Task SkipPrevious();

    Task LoadingPlaylist();

    Task EnableSource(LibrarySource source);
    Task DisableSource(SourceException ex);

    Task Play(PlaylistDefinition playlistDefinition);
}
