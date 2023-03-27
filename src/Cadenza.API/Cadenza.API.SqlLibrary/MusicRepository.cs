using Cadenza.API.Interfaces.Repositories;
using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.API.SqlLibrary;
internal class MusicRepository : IMusicRepository
{
    private readonly ILibraryReader _libraryReader;
    private readonly ITrackAdder _trackAdder;
    private readonly ITrackRemover _trackRemover;

    public MusicRepository(ITrackAdder trackAdder, ITrackRemover trackRemover, ILibraryReader libraryReader)
    {
        _trackAdder = trackAdder;
        _trackRemover = trackRemover;
        _libraryReader = libraryReader;
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

    public Task UpdateAlbum(LibrarySource source, ItemUpdates updates)
    {
        throw new NotImplementedException();
    }

    public Task UpdateArtist(ItemUpdates updates)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTrack(LibrarySource source, ItemUpdates updates)
    {
        throw new NotImplementedException();
    }
}
