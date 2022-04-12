namespace Cadenza.Source.Spotify.Interfaces;

public interface IDeviceHelper
{
    Task<string> GetDeviceId(bool forceCreateNew);
}