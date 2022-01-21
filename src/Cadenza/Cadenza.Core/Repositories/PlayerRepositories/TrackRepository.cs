namespace Cadenza.Core
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ITrackRepositoryUpdater _baseRepository;
        private readonly IEnumerable<ISourceLibrary> _sources;

        public TrackRepository(ITrackRepositoryUpdater baseRepository, IEnumerable<ISourceLibrary> sources)
        {
            _baseRepository = baseRepository;
            _sources = sources;
        }

        public Task<List<AlbumTrackInfo>> GetAlbumTracks(LibrarySource source, string id)
        {
            throw new NotImplementedException();
        }

        public async Task<TrackFull> GetDetails(LibrarySource source, string id)
        {
            var track = await _baseRepository.GetDetails(source, id);

            if (track == null)
            {
                var sourceRepository = _sources.Single(s => s.Source == source);

                var dbTrack = await sourceRepository.GetFullTrack(id);

                // Can I do this in bg after returning track
                await _baseRepository.AddTrack(dbTrack);

                return dbTrack;
            }

            return track;
        }

        public async Task<TrackSummary> GetSummary(LibrarySource source, string id)
        {
            var track = await _baseRepository.GetSummary(source, id);

            if (track == null)
            {
                var sourceRepository = _sources.Single(s => s.Source == source);

                var dbTrack = await sourceRepository.GetTrack(id);

                // Can I do this in bg after returning track
                await _baseRepository.AddTrack(dbTrack);

                return dbTrack;
            }

            return track;
        } 
    }
}
