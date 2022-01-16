using Cadenza.Domain;

namespace Cadenza.Library;

public interface ILibrary
{
    Task<ICollection<Artist>> GetAlbumArtists();
    Task<ICollection<Track>> GetAllTracks();
    Task<ArtistFull> GetAlbumArtist(string id);
    Task<PlayingTrack> GetTrack(string id);
    Task<FullTrack> GetFullTrack(string id);
}
