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
                await _queueRepository.AddTrackUpdateRequest(updatedTrack.TrackId);
                await _updateRepository.UpdateAlbumTrack(updatedTrack);
            }
        }
    }

    public async Task UpdateTrack(UpdateTrackDTO request)
    {
        await _queueRepository.AddTrackUpdateRequest(request.UpdatedTrack.TrackId);
        await _updateRepository.UpdateTrack(request.UpdatedTrack);
    }

    public async Task UpdateAlbum(UpdateAlbumDTO request)
    {
        await _queueRepository.AddAlbumUpdateRequest(request.UpdatedAlbum.AlbumId);
        await _updateRepository.UpdateAlbum(request.UpdatedAlbum);
    }

    public async Task UpdateArtist(UpdateArtistDTO request)
    {
        await _queueRepository.AddArtistUpdateRequest(request.UpdatedArtist.ArtistId);
        await _updateRepository.UpdateArtist(request.UpdatedArtist);
    }
}