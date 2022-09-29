namespace Cadenza.Web.Common.Interfaces;

public interface IAppController
{
    Task Pause();
    Task Resume();
    Task SkipNext();
    Task SkipPrevious();

    Task LoadingPlaylist();

    Task Play(PlaylistDefinition playlistDefinition);

    Task View(ViewItem item);
}
