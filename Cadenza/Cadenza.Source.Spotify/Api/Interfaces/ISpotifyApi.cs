namespace Cadenza.Source.Spotify;

internal interface ISpotifyApi
{
    Task Put(string urlFormat, object data = null);
    Task<T> Get<T>(string url);
}