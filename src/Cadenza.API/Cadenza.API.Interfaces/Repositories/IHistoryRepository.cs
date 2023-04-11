namespace Cadenza.API.Interfaces.Repositories;

public interface IHistoryRepository
{
    Task<List<RecentAlbum>> GetRecentAlbums(int maxItems);
    Task<List<string>> GetRecentTags(int maxItems);

    Task LogLibraryPlay();
    Task LogArtistPlay(string nameId);
    Task LogAlbumPlay(int albumId);
    Task LogTrackPlay(string idFromSource);
    Task LogGroupingPlay(Grouping grouping);
    Task LogGenrePlay(string genre);
    Task LogTagPlay(string tag);
}