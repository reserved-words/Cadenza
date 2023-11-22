namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface ILibraryReader
{
    Task<FullLibraryDTO> Get();

    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<ArtworkImage> GetAlbumArtwork(int id);
    Task<ArtworkImage> GetArtistImage(int id);
    Task<List<string>> GetAlbumTrackSourceIds(int albumId);
    Task<List<string>> GetArtistTrackSourceIds(int artistId);
    Task<string> GetTrackIdFromSource(int trackId);

    Task<List<int>> GetAllTrackIds();
    Task<List<int>> GetAbumTrackIds(int albumId);
    Task<List<int>> GetArtistTrackIds(int artistId);
    Task<List<int>> GetGenreTrackIds(string genre);
    Task<List<int>> GetGroupingTrackIds(int groupingId);
    Task<List<int>> GetTagTrackIds(string tag);
}
