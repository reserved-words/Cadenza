namespace Cadenza.Core;

public interface ISpotifyLibrary
{
    Task<FullLibrary> Get();
    Task<List<SpotifyArtist>> Search(string text);
    Task<List<SpotifyAlbum>> GetArtistAlbums(string artistId);
}

public class SpotifyArtist
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; } // Check if can get this
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
}