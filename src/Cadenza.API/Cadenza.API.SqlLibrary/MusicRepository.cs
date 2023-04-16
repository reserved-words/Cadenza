using Cadenza.API.Interfaces.Repositories;
using Cadenza.API.SqlLibrary.Interfaces;

namespace Cadenza.API.SqlLibrary;

internal class MusicRepository : IMusicRepository
{
    private readonly ILibraryReader _libraryReader;
    private readonly ILibraryUpdater _libraryUpdater;
    private readonly ITrackAdder _trackAdder;
    private readonly ITrackRemover _trackRemover;

    public MusicRepository(ITrackAdder trackAdder, ITrackRemover trackRemover, ILibraryReader libraryReader, ILibraryUpdater libraryUpdater)
    {
        _trackAdder = trackAdder;
        _trackRemover = trackRemover;
        _libraryReader = libraryReader;
        _libraryUpdater = libraryUpdater;
    }

    public async Task AddTrack(LibrarySource source, SyncTrack track)
    {
        await _trackAdder.AddTrack(source, track);
    }

    public async Task<FullLibrary> Get()
    {
        return await _libraryReader.Get();
    }

    public async Task<List<string>> GetAlbumTrackSourceIds(int albumId)
    {
        return await _libraryReader.GetAlbumTrackSourceIds(albumId);
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        return await _libraryReader.GetAllTracks(source);
    }

    public async Task<List<string>> GetArtistTrackSourceIds(int artistId)
    {
        return await _libraryReader.GetArtistTrackSourceIds(artistId);
    }

    public async Task<string> GetTrackIdFromSource(int trackId)
    {
        return await _libraryReader.GetTrackIdFromSource(trackId);
    }

    public async Task RemoveTrack(int id)
    {
        await _trackRemover.RemoveTrack(id);
    }

    public async Task RemoveTracks(List<string> idsFromSource)
    {
        await _trackRemover.RemoveTracks(idsFromSource);
    }

    public async Task UpdateAlbum(ItemUpdateRequest request)
    {
        await _libraryUpdater.UpdateAlbum(request);
    }

    public async Task UpdateArtist(ItemUpdateRequest request)
    {
        await _libraryUpdater.UpdateArtist(request);
    }

    public async Task UpdateTrack(ItemUpdateRequest request)
    {
        await _libraryUpdater.UpdateTrack(request);
    }
}
