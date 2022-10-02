namespace Cadenza.Web.Common.Interfaces.Store;

public interface ICurrentTrackStore
{
    Task<LibrarySource?> GetCurrentSource();
    Task<TrackFull> GetCurrentTrack();
    Task SetCurrentTrack(string id);
}