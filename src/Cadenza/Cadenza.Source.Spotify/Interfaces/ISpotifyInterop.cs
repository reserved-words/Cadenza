namespace Cadenza.Source.Spotify.Interfaces;

internal interface ISpotifyInterop
{
    Task<bool> ConnectPlayer(string accessToken);
}
