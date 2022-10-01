namespace Cadenza.Web.Common.Interfaces;

public interface IAppController
{
    Task SkipNext();
    Task SkipPrevious();

    Task LoadingPlaylist();

    Task Play(PlaylistDefinition playlistDefinition);

    Task View(ViewItem item);
}
