namespace Cadenza.Web.Api.Interfaces;

public interface ITrackApi
{
    Task<TrackFullVM> GetTrack(int id);
}
