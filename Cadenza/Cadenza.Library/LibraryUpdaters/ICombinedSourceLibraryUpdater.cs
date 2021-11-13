namespace Cadenza.Library;

public interface ICombinedSourceLibraryUpdater : ILibraryUpdater
{
    Task<bool> UpdateTrack(TrackUpdate update);
}
