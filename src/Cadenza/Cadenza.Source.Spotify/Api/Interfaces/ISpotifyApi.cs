namespace Cadenza.Source.Spotify;

public interface ISpotifyApi
{
    Task Put(string urlFormat, object data = null);
    Task<T> Get<T>(string url);
}