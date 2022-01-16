namespace Cadenza.Library;

public interface ICombinedSourceLibraryUpdater : ILibraryUpdater
{
    Task<bool> UpdateTrack(TrackInfo track);
}
