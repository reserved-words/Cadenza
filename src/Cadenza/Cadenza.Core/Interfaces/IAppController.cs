using Cadenza.Core.Model;
using Cadenza.Core.Playlists;

namespace Cadenza.Core.App;

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
