﻿using Cadenza.Database;

namespace Cadenza.Player
{

    public class TrackRepository : ITrackRepository
    {
        private readonly ITrackRepositoryUpdater _baseRepository;

        private Dictionary<LibrarySource, ISourceRepository> _sources;

        public TrackRepository(ITrackRepositoryUpdater baseRepository, Dictionary<LibrarySource, ISourceRepository> sources)
        {
            _baseRepository = baseRepository;
            _sources = sources;
        }

        public async Task<PlayingTrack> GetSummary(LibrarySource source, string id)
        {
            var track = await _baseRepository.GetSummary(source, id);

            if (track == null)
            {
                var sourceRepository = _sources[source];
                var dbTrack = await sourceRepository.GetTrack(id);
                await _baseRepository.AddTrack(dbTrack);
                track = await _baseRepository.GetSummary(source, id);
            }

            return track;
        } 
    }
}
