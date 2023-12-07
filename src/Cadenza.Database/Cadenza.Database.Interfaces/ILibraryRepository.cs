namespace Cadenza.Database.Interfaces;

public interface ILibraryRepository
{
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<List<string>> GetAlbumTrackSourceIds(int albumId);
    Task<List<string>> GetArtistTrackSourceIds(int artistId);

    Task<List<TaggedItemDTO>> GetTaggedItems(string tag);

    Task<string> GetTrackIdFromSource(int trackId);
    Task<ArtistDetailsDTO> GetArtist(int id);
    Task<AlbumDetailsDTO> GetAlbum(int id);
    Task<TrackFullDTO> GetTrack(int id);

    Task<List<AlbumDiscDTO>> GetAlbumTracks(int id);

    Task<List<AlbumDTO>> GetArtistAlbums(int artistId);
    Task<List<AlbumDTO>> GetAlbumsFeaturingArtist(int artistId);

    Task<List<ArtistDTO>> GetArtistsByGrouping(int groupingId);
    Task<List<ArtistDTO>> GetArtistsByGenre(string genre);
}