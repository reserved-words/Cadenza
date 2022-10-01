namespace Cadenza.Web.Common.Interfaces;

public interface ICurrentTrackStore
{
    Task<LibrarySource?> GetCurrentSource();
    Task<TrackFull> GetCurrentTrack();
    Task StoreCurrentTrack(TrackFull track);
}