namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface ILibraryReader
{
    Task<FullLibrary> Get();
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<ArtworkImage> GetAlbumArtwork(int id);
    Task<ArtworkImage> GetArtistImage(int id);
    Task<List<string>> GetAlbumTrackSourceIds(int albumId);
    Task<List<string>> GetArtistTrackSourceIds(int artistId);
    Task<string> GetTrackIdFromSource(int trackId);
}
