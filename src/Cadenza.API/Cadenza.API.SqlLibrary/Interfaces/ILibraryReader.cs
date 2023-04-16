namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface ILibraryReader
{
    Task<FullLibrary> Get(LibrarySource? source);
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<ArtworkImage> GetAlbumArtwork(int id);
    Task<ArtworkImage> GetArtistImage(int id);
}
