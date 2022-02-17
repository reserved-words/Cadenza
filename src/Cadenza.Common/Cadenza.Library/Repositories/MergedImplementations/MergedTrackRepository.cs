namespace Cadenza.Library;

public class MergedTrackRepository : IMergedTrackRepository
{
    private readonly IEnumerable<ISourceTrackRepository> _sources;

    public MergedTrackRepository(IEnumerable<ISourceTrackRepository> sources)
    {
        _sources = sources;
    }

    public async Task<TrackFull> GetTrack(LibrarySource source, string id)
    {
        var sourceRepository = _sources.Single(s => s.Source == source);
        return await sourceRepository.GetTrack(id);
    }
}
