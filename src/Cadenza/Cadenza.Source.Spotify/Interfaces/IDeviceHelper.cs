namespace Cadenza.Source.Spotify.Interfaces;

public interface IDeviceHelper
{
    Task<string> GetDeviceId(string accessToken, bool forceCreateNew);
}