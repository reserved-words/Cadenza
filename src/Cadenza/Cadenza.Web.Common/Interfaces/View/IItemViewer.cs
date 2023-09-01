namespace Cadenza.Web.Common.Interfaces.View;

public interface IItemViewer
{
    Task ViewAlbum(int id, string title);
    Task ViewArtist(int id, string name);
    Task ViewGenre(string id);
    Task ViewGrouping(Grouping grouping);
    Task ViewPlaying(PlaylistId playlist);
    Task ViewSearchResult(PlayerItem item);
    Task ViewTag(string id);
    Task ViewTrack(int id, string title);
}