namespace Cadenza.API.SqlLibrary.Services;

internal class TrackRemover : ITrackRemover
{
    private readonly IDataDeletionService _deletionService;

    public TrackRemover(IDataDeletionService deletionService)
    {
        _deletionService = deletionService;
    }

    public async Task RemoveTrack(int id)
    {
        await _deletionService.DeleteTrackById(id);
        await _deletionService.DeleteEmptyDiscs();
        await _deletionService.DeleteEmptyAlbums();
        await _deletionService.DeleteEmptyArtists();
    }

    public async Task RemoveTracks(List<string> idsFromSource)
    {
        foreach (var idFromSource in idsFromSource)
        {
            await _deletionService.DeleteTrackByIdFromSource(idFromSource);
        }

        await _deletionService.DeleteEmptyDiscs();
        await _deletionService.DeleteEmptyAlbums();
        await _deletionService.DeleteEmptyArtists();
    }
}
