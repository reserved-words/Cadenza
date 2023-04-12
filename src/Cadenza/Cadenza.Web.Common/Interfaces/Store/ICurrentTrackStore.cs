namespace Cadenza.Web.Common.Interfaces.Store;

public interface ICurrentTrackStore
{
    Task<LibrarySource?> GetCurrentSource();
    Task<TrackFull> GetCurrentTrack();
    Task<string> GetCurrentTrackId();
    Task SetCurrentTrack(string id);
}