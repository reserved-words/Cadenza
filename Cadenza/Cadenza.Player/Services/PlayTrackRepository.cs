﻿using Cadenza.Database;

namespace Cadenza.Player
{
    public class PlayTrackRepository : IPlayTrackRepository
    {
        private readonly IPlayTrackRepositoryUpdater _baseRepository;

        private Dictionary<LibrarySource, ISourceRepository> _sources;

        public PlayTrackRepository(IPlayTrackRepositoryUpdater baseRepository, Dictionary<LibrarySource, ISourceRepository> sources)
        {
            _baseRepository = baseRepository;
            _sources = sources;
        }

        public Task<List<PlayTrack>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PlayTrack>> GetByAlbum(LibrarySource source, string artistId, string albumId)
        {
            var tracks = await _baseRepository.GetByAlbum(source, artistId, albumId);

            if (tracks == null || !tracks.Any())
            {
                var sourceRepository = _sources[source];
                var dbTracks = await sourceRepository.GetAlbumTracks(artistId, albumId);
                await _baseRepository.AddAlbumTracks(source, albumId, dbTracks);
                tracks = await _baseRepository.GetByAlbum(source, artistId, albumId);
            }

            return tracks;
        }

        public async Task<List<PlayTrack>> GetByArtist(string id)
        {
            var tracks = await _baseRepository.GetByArtist(id);

            if (tracks == null || !tracks.Any())
            {
                foreach (var source in _sources.Keys)
                {
                    var sourceRepository = _sources[source];
                    var dbTracks = await sourceRepository.GetArtistTracks(id);
                    await _baseRepository.AddArtistTracks(source, id, dbTracks);
                }

                tracks = await _baseRepository.GetByArtist(id);
            }

            return tracks;
        }
    }
}
