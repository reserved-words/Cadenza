namespace Cadenza.Local.API.Common.Controllers;

public interface IPlayService
{
    Task<string> GetTrackPlayPath(string id);
}
