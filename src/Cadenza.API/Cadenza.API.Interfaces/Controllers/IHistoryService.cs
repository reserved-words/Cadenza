namespace Cadenza.API.Interfaces.Controllers;

public interface IHistoryService
{
    Task<List<RecentAlbum>> GetRecentAlbums(int maxItems);
    Task<List<string>> GetRecentTags(int maxItems);
    Task LogLibraryPlay();
    Task LogArtistPlay(int artistId);
    Task LogAlbumPlay(int albumId);
    Task LogTrackPlay(int trackId);
    Task LogGroupingPlay(int groupingId);
    Task LogGenrePlay(string genre);
    Task LogTagPlay(string tag);
}