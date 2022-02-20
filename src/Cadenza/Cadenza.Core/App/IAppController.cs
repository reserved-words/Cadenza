using Cadenza.Core.Model;

namespace Cadenza.Core;

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
