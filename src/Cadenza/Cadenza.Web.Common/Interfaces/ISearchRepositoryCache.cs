namespace Cadenza.Web.Common.Interfaces;

public interface ISearchRepositoryCache
{
    List<PlayerItem> Items { get; set; }

    event EventHandler UpdateCompleted;
    event EventHandler UpdateStarted;

    void AddAlbums(List<PlayerItem> items);
    void AddArtists(List<PlayerItem> items);
    void AddGenres(List<PlayerItem> items);
    void AddGroupings(List<PlayerItem> items);
    void AddPlaylists(List<PlayerItem> items);
    void AddTracks(List<PlayerItem> items);
    void Clear();
    void FinishUpdate();
    void StartUpdate();
}