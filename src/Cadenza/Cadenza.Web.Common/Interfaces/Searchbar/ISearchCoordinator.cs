namespace Cadenza.Web.Common.Interfaces.Searchbar;

public interface ISearchCoordinator
{
    void AddAlbums(List<PlayerItem> items);
    void AddArtists(List<PlayerItem> items);
    void AddGenres(List<PlayerItem> items);
    void AddGroupings(List<PlayerItem> items);
    void AddPlaylists(List<PlayerItem> items);
    void AddTracks(List<PlayerItem> items);
    void Clear();
    Task FinishUpdate();
    Task StartUpdate();
}