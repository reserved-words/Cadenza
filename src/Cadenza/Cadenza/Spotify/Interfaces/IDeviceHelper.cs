namespace Cadenza.Source.Spotify.Player;

public interface IDeviceHelper
{
    Task<string> GetDeviceId(string accessToken, bool forceCreateNew);
}