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

    public async Task AddTrack(LibrarySource source, TrackFull track)
    {
        await _trackAdder.AddTrack(source, track);
    }

    public async Task<FullLibrary> Get(LibrarySource? source)
    {
        return await _libraryReader.Get(source);
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        return await _libraryReader.GetAllTracks(source);
    }

    public async Task RemoveTracks(LibrarySource source, List<string> ids)
    {
        await _trackRemover.RemoveTracks(source, ids);
    }

    public async Task UpdateAlbum(LibrarySource source, ItemUpdates updates)
    {
        await _libraryUpdater.UpdateAlbum(updates);
    }

    public async Task UpdateArtist(ItemUpdates updates)
    {
        await _libraryUpdater.UpdateArtist(updates);
    }

    public async Task UpdateTrack(LibrarySource source, ItemUpdates updates)
    {
        await _libraryUpdater.UpdateTrack(updates);
    }
}
