using Cadenza.API.SqlLibrary.Interfaces;

namespace Cadenza.API.SqlLibrary.Services;

internal class TrackRemover : ITrackRemover
{
    private readonly IDataDeletionService _deletionService;

    public TrackRemover(IDataDeletionService deletionService)
    {
        _deletionService = deletionService;
    }

    public async Task RemoveTracks(LibrarySource source, List<string> ids)
    {
        foreach (var id in ids)
        {
            await _deletionService.DeleteTrack(source, id);
        }

        await _deletionService.DeleteEmptyDiscs();
        await _deletionService.DeleteEmptyAlbums();
        await _deletionService.DeleteEmptyArtists();
    }
}
