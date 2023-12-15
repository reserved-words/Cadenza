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

    public async Task UpdateTrack(UpdatedTrackPropertiesDTO update)
    {
        await _updateRepository.UpdateTrack(update);
        await _queueRepository.AddTrackUpdateRequest(update.TrackId);
    }

    public async Task UpdateAlbum(AlbumUpdateDTO update)
    {
        // TODO: Ideally do this all in one transaction so can roll back all and display one error message if it fails

        if (update.UpdatedAlbum != null)
        {
            await _updateRepository.UpdateAlbum(update.UpdatedAlbum);
            await _queueRepository.AddAlbumUpdateRequest(update.AlbumId);
        }

        if (update.UpdatedAlbumTracks != null)
        {
            foreach (var updatedTrack in update.UpdatedAlbumTracks)
            {
                await _updateRepository.UpdateAlbumTrack(updatedTrack);
                await _queueRepository.AddTrackUpdateRequest(updatedTrack.TrackId);
            }
        }

        if (update.RemovedTracks != null)
        {
            foreach (var removedTrackId in update.RemovedTracks)
            {
                await _queueRepository.AddRemovalRequest(removedTrackId);
                await _updateRepository.RemoveTrack(removedTrackId);
            }
        }
    }

    public async Task UpdateArtist(ArtistUpdateDTO update)
    {
        // TODO: Ideally do this all in one transaction so can roll back all and display one error message if it fails

        if (update.UpdatedArtist != null)
        {
            await _updateRepository.UpdateArtist(update.UpdatedArtist);
            await _queueRepository.AddArtistUpdateRequest(update.ArtistId);
        }

        if (update.UpdatedArtistReleases != null)
        {
            foreach (var updatedRelease in update.UpdatedArtistReleases)
            {
                await _updateRepository.UpdateArtistRelease(updatedRelease);
                await _queueRepository.AddAlbumUpdateRequest(updatedRelease.AlbumId);
            }
        }
    }
}