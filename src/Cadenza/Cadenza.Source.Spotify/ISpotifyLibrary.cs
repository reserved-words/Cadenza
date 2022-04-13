using Cadenza.Domain;

namespace Cadenza.Source.Spotify;

public interface ISpotifyLibrary
{
    Task<FullLibrary> Get();
    Task AddAlbum(string id);
    Task AddPlaylist(string id);
}
