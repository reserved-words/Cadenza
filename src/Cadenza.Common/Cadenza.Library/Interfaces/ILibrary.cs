namespace Cadenza.Library;

public interface ILibrary
{
    Task<IEnumerable<ArtistInfo>> GetArtists();
    Task<IEnumerable<AlbumInfo>> GetAlbums();
    Task<PlayingTrack> GetTrack(string id);
    Task<IEnumerable<string>> GetAlbumTracks(string artistId, string albumId);
    Task<IEnumerable<string>> GetArtistTracks(string id);
    Task<IEnumerable<string>> GetAllTracks();
    Task<FullTrack> GetFullTrack(string id);
}
