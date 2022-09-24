using Cadenza.Web.Common.Model;
using Cadenza.Web.Core.Playlists;

namespace Cadenza.Web.Core.Interfaces;

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
