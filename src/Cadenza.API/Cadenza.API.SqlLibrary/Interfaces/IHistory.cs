using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface IHistory
{
    Task<List<GetRecentAlbumsResult>> GetRecentAlbums(int maxItems);
    Task<List<GetRecentTagsResult>> GetRecentTags(int maxItems);
    Task LogLibraryPlay();
    Task LogArtistPlay(int artistId);
    Task LogAlbumPlay(int albumId);
    Task LogTrackPlay(int trackId);
    Task LogGroupingPlay(int groupingId);
    Task LogGenrePlay(string genre);
    Task LogTagPlay(string tag);
}
