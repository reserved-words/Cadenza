namespace Cadenza.Core;

public interface ISpotifyLibrary
{
    Task<FullLibrary> Get();
    Task AddAlbum(string id);
    Task AddPlaylist(string id);
}

public interface ISpotifySearcher
{
    Task<List<SpotifyArtist>> SearchArtist(string artistName);
    Task<List<SpotifyAlbum>> GetArtistAlbums(string artistId);
    Task<List<SpotifyPlaylist>> GetArtistPlaylists(string artistName);
}

public class SpotifyArtist
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class SpotifyAlbum
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Year { get; set; }
    public string ArtworkUrl { get; set; }
}

public class SpotifyArtistProfile
{
    public SpotifyArtist Artist { get; set; }
    public List<SpotifyAlbum> Albums { get; set; }
    public List<SpotifyPlaylist> Playlists { get; set; }
}

public class SpotifyPlaylist
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string ArtworkUrl { get; set; }
    public string CreatedBy { get; set; }
}