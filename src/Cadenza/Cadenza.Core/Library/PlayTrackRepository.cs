using Cadenza.Common;
using Cadenza.Domain;
using Cadenza.Library;

namespace Cadenza.Core
{
    public class PlayTrackRepository : IPlayTrackRepository
    {
        private readonly IPlayTrackRepositoryUpdater _baseRepository;

        private IEnumerable<ISourceLibrary> _sources;

        public PlayTrackRepository(IPlayTrackRepositoryUpdater baseRepository, IEnumerable<ISourceLibrary> sources)
        {
            _baseRepository = baseRepository;
            _sources = sources;
        }

        public async Task<IEnumerable<PlayTrack>> GetAll()
        {
            var tracks = await _baseRepository.GetAll();

            if (tracks == null || !tracks.Any())
            {
                foreach (var source in _sources)
                {
                    var dbTracks = await source.GetAllTracks();
                    await _baseRepository.AddAllTracks(source.Source, dbTracks);
                }

                tracks = await _baseRepository.GetAll();
            }

            return tracks;

        }

        public async Task<IEnumerable<PlayTrack>> GetByAlbum(LibrarySource source, string artistId, string albumId)
        {
            // speed this up - inserting could be done in the background

            var tracks = await _baseRepository.GetByAlbum(source, artistId, albumId);

            if (tracks == null || !tracks.Any())
            {
                var sourceRepository = _sources.Single(s => s.Source == source);
                var dbTracks = await sourceRepository.GetAlbumTracks(artistId, albumId);
                await _baseRepository.AddAlbumTracks(source, albumId, dbTracks);
                tracks = await _baseRepository.GetByAlbum(source, artistId, albumId);
            }

            return tracks;
        }

        public async Task<IEnumerable<PlayTrack>> GetByArtist(string id)
        {
            // speed this up - inserting could be done in the background

            var tracks = await _baseRepository.GetByArtist(id);

            if (tracks == null || !tracks.Any())
            {
                foreach (var source in _sources)
                {
                    var dbTracks = await source.GetArtistTracks(id);
                    await _baseRepository.AddArtistTracks(source.Source, id, dbTracks);
                }

                tracks = await _baseRepository.GetByArtist(id);
            }

            return tracks;
        }
    }
}
