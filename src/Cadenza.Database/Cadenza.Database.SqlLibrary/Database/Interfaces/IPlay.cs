namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface IPlay
{
    Task<List<int>> GetAllTrackIds();
    Task<List<int>> GetAlbumTrackIds(int albumId);
    Task<List<int>> GetArtistTrackIds(int artistId);
    Task<List<int>> GetGenreTrackIds(string genre, int groupingId);
    Task<List<int>> GetGroupingTrackIds(int groupingId);
    Task<List<int>> GetTagTrackIds(string tag);
}
