namespace Cadenza.API.Interfaces.Controllers;

public interface IHistoryService
{
    Task<List<RecentAlbum>> GetRecentAlbums(int maxItems);
    Task LogLibraryPlay();
    Task LogArtistPlay(string nameId);
    Task LogAlbumPlay(int albumId);
    Task LogTrackPlay(string idFromSource);
    Task LogGroupingPlay(Grouping grouping);
    Task LogGenrePlay(string genre);
    Task LogTagPlay(string tag);
}