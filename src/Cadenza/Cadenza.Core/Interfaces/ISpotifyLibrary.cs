namespace Cadenza.Core;

public interface ISpotifyLibrary
{
    Task<FullLibrary> Get();
    Task<List<SpotifyArtist>> Search(string text);
}

public class SpotifyArtist
{
    public string Id { get; set; }
    public string Name { get; set; }
}