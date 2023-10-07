namespace Cadenza.API.Interfaces.Library;

public interface ITrackRepository
{
    Task<TrackFullDTO> GetTrack(int id);
}
