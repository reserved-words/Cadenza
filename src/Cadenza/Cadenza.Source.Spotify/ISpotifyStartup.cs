namespace Cadenza.Source.Spotify;

public interface ISpotifyStartup
{
    Task<bool> ConnectPlayer(string accessToken);
}