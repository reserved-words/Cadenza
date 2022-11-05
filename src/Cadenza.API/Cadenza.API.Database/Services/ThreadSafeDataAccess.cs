namespace Cadenza.API.Database.Services;

internal class ThreadSafeDataAccess : IDataAccess
{
    private readonly IDataAccess _dataAccess;

    public ThreadSafeDataAccess(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }


    private static object obj = new object();

    public Task<FullLibrary> GetAll(LibrarySource? source)
    {
        lock (obj)
        {
            return _dataAccess.GetAll(source);
        }
    }

    public Task<List<ArtistInfo>> GetArtists()
    {
        lock (obj)
        {
            return _dataAccess.GetArtists();
        }
    }

    public Task<List<AlbumInfo>> GetAlbums(LibrarySource source)
    {
        lock (obj)
        {
            return _dataAccess.GetAlbums(source);
        }
    }

    public Task<List<AlbumTrackLink>> GetAlbumTracks(LibrarySource source)
    {
        lock (obj)
        {
            return _dataAccess.GetAlbumTracks(source);
        }
    }

    public Task<List<TrackInfo>> GetTracks(LibrarySource source)
    {
        lock (obj)
        {
            return _dataAccess.GetTracks(source);
        }
    }

    public Task<List<ItemUpdates>> GetUpdates(LibrarySource source)
    {
        lock (obj)
        {
            return _dataAccess.GetUpdates(source);
        }
    }

    public Task UpdateLibrary(LibrarySource source, Action<FullLibrary> library)
    {
        lock (obj)
        {
            return _dataAccess.UpdateLibrary(source, library);
        }
    }

    public Task UpdateUpdates(LibrarySource source, Action<List<ItemUpdates>> updates)
    {
        lock (obj)
        {
            return _dataAccess.UpdateUpdates(source, updates);
        }
    }

    public Task UpdateAlbum(LibrarySource source, string id, Action<AlbumInfo> update)
    {
        lock (obj)
        {
            _dataAccess.UpdateAlbum(source, id, update);
            return Task.CompletedTask;
        }
    }

    public Task UpdateArtist(string id, Action<ArtistInfo> update)
    {
        lock (obj)
        {
            _dataAccess.UpdateArtist(id, update);
            return Task.CompletedTask;
        }
    }

    public Task UpdateTrack(LibrarySource source, string id, Action<TrackInfo> update)
    {
        lock (obj)
        {
            _dataAccess.UpdateTrack(source, id, update);
            return Task.CompletedTask;
        }
    }
}
