namespace Cadenza.Local.API;

public interface ILibraryService
{
    Task<ArtistFull> GetAlbumArtist(string artistId);
    Task<ICollection<Artist>> GetAlbumArtists();
    Task<TrackFull> GetTrack(string id);
    Task<TrackSummary> GetTrackSummary(string id);

    Task<ICollection<ArtistInfo>> GetArtists();
    Task<ICollection<AlbumInfo>> GetAlbums();
    Task<ICollection<TrackInfo>> GetTracks();
    Task<ICollection<AlbumTrackLink>> GetAlbumTrackLinks();
}
