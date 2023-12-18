namespace Cadenza.Database.Interfaces;

public interface ILibraryRepository
{
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<List<string>> GetAlbumTrackSourceIds(int albumId);
    Task<List<string>> GetArtistTrackSourceIds(int artistId);

    Task<List<TaggedItemDTO>> GetTaggedItems(string tag);

    Task<string> GetTrackIdFromSource(int trackId);
    Task<ArtistDTO> GetArtist(int id);
    Task<ArtistFullDTO> GetFullArtist(int id, bool includeAlbumsByOtherArtists);
    Task<AlbumDTO> GetAlbum(int id);
    Task<AlbumFullDTO> GetAlbumFull(int id);
    Task<TrackDetailsDTO> GetTrack(int id);
    Task<TrackFullDTO> GetTrackFull(int id);

    Task<List<ArtistDTO>> GetArtistsByGrouping(int groupingId);
    Task<List<ArtistDTO>> GetArtistsByGenre(string genre);
}