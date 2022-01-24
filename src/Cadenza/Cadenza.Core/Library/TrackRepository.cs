namespace Cadenza.Core
{
    public class TrackRepository : ITrackRepository
    {
        private readonly IEnumerable<ISourceLibrary> _sources;

        public TrackRepository(IEnumerable<ISourceLibrary> sources)
        {
            _sources = sources;
        }

        public Task<List<AlbumTrackInfo>> GetAlbumTracks(LibrarySource source, string id)
        {
            throw new NotImplementedException();
        }

        public async Task<TrackFull> GetDetails(LibrarySource source, string id)
        {
            var sourceRepository = _sources.Single(s => s.Source == source);
            return await sourceRepository.GetFullTrack(id);
        }

        public async Task<TrackSummary> GetSummary(LibrarySource source, string id)
        {
            var sourceRepository = _sources.Single(s => s.Source == source);
            return await sourceRepository.GetTrack(id);
        } 
    }
}
