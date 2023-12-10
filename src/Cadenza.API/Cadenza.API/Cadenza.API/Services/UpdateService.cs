using Cadenza.Common.DTO;

namespace Cadenza.API.Services;

internal class UpdateService : IUpdateService
{
    private readonly IQueueRepository _queueRepository;
    private readonly IUpdateRepository _updateRepository;

    public UpdateService(IQueueRepository queueRepository, IUpdateRepository updateRepository)
    {
        _queueRepository = queueRepository;
        _updateRepository = updateRepository;
    }

    public async Task UpdateAlbumTracks(UpdateAlbumTracksDTO request)
    {
        foreach (var originalTrack in request.OriginalTracks)
        {
            var updatedTrack = request.UpdatedTracks
                .SingleOrDefault(t => t.TrackId == originalTrack.TrackId);

            if (updatedTrack == null)
            {
                await _queueRepository.AddRemovalRequest(originalTrack.TrackId);
                await _updateRepository.RemoveTrack(originalTrack.TrackId);
            }
            else
            {
                await _updateRepository.UpdateAlbumTrack(updatedTrack);
                await _queueRepository.AddTrackUpdateRequest(updatedTrack.TrackId);
            }
        }
    }

    public async Task UpdateTrack(UpdatedTrackPropertiesDTO update)
    {
        await _updateRepository.UpdateTrack(update);
        await _queueRepository.AddTrackUpdateRequest(update.TrackId);
    }

    public async Task UpdateAlbum(UpdatedAlbumPropertiesDTO update)
    {
        await _updateRepository.UpdateAlbum(update);
        await _queueRepository.AddAlbumUpdateRequest(update.AlbumId);
    }

    public async Task UpdateArtist(UpdatedArtistPropertiesDTO update)
    {
        await _updateRepository.UpdateArtist(update);
        await _queueRepository.AddArtistUpdateRequest(update.ArtistId);
    }
}