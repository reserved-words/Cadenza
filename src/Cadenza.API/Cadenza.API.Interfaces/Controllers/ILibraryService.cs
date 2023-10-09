namespace Cadenza.API.Interfaces.Controllers;

public interface ILibraryService
{
    Task<AlbumDetailsDTO> Album(int id);
    Task<List<AlbumDTO>> AlbumsFeaturingArtist(int id);
    Task<List<AlbumTrackDTO>> AlbumTracks(int id);
    Task<ArtistDetailsDTO> Artist(int id);
    Task<List<AlbumDTO>> ArtistAlbums(int id);
    Task<List<TrackDTO>> ArtistTracks(int id);
    Task<List<ArtistDTO>> Artists();
    Task<List<ArtistDTO>> GenreArtists(string id);
    Task<List<ArtistDTO>> GroupingArtists(int id);
    Task<List<PlayerItemDTO>> Tag(string id);
    Task<TrackFullDTO> Track(int id);
}