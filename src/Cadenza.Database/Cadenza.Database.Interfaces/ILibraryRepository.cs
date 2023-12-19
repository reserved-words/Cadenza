namespace Cadenza.Database.Interfaces;

public interface ILibraryRepository
{
    Task<List<TaggedItemDTO>> GetTaggedItems(string tag);

    Task<ArtistFullDTO> GetFullArtist(int id, bool includeAlbumsByOtherArtists);
    Task<AlbumFullDTO> GetAlbumFull(int id);
    Task<TrackDetailsDTO> GetTrack(int id);
    Task<TrackFullDTO> GetTrackFull(int id);

    Task<List<ArtistDTO>> GetArtistsByGrouping(string grouping);
    Task<GenreDTO> GetArtistsByGenre(string grouping, string genre);
    Task<List<string>> GetGroupings();
}