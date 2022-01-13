namespace Cadenza.Local.API;

public interface IPlayService
{
    Task<string> GetTrackPlayPath(string id);
}
