using Cadenza.Database.SqlLibrary.Database.Interfaces;

namespace Cadenza.Database.SqlLibrary.Repositories;

internal class SyncRepository : ISyncRepository
{
    private readonly ILibrary _library;

    public SyncRepository(ILibrary library)
    {
        _library = library;
    }

    public async Task<List<string>> GetAlbumTrackSourceIds(int albumId)
    {
        return await _library.GetAlbumTrackSourceIds(albumId);
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        return await _library.GetTrackSourceIds(source);
    }

    public async Task<List<string>> GetArtistTrackSourceIds(int artistId)
    {
        return await _library.GetArtistTrackSourceIds(artistId);
    }

    public async Task<string> GetTrackIdFromSource(int trackId)
    {
        return await _library.GetTrackIdFromSource(trackId);
    }
}
