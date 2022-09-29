using Cadenza.Domain.Enums;
using Cadenza.Domain.Model;
using Cadenza.Web.Common.Model;

namespace Cadenza.Web.Common.Interfaces;

public interface IItemViewer
{
    Task ViewSearchResult(PlayerItem item);
    Task ViewPlaying(PlaylistId playlist);
    Task ViewGrouping(Grouping id);
    Task ViewGenre(string id);
    Task ViewArtist(string id, string name);
    Task ViewArtist(string name);
    Task ViewAlbum(string id, string title);
    Task ViewTrack(string id, string title);
}