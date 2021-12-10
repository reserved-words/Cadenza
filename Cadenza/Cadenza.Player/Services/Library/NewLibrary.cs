using Cadenza.Database;

namespace Cadenza.Player;

public class NewLibrary : IViewModelLibrary, ILibraryController
{
    public event AlbumUpdatedEventHandler AlbumUpdated;
    public event ArtistUpdatedEventHandler ArtistUpdated;
    public event TrackUpdatedEventHandler TrackUpdated;

    private readonly IStoreGetter _storeGetter;
    private readonly ICombinedSourceLibrary _library;
    private readonly ICombinedSourceLibraryUpdater _updater;

    public NewLibrary(IStoreGetter store, ICombinedSourceLibrary library, ICombinedSourceLibraryUpdater updater)
    {
        _storeGetter = store;
        _library = library;
        _updater = updater;
    }

    public async Task<List<Artist>> GetAlbumArtists()
    {
        var enabledSources = await GetEnabledSources();
        var artists = await _library.GetAlbumArtists(enabledSources);
        return artists
            .OrderBy(a => a.Id)
            .ToList();
    }

    //public async Task<List<PlayTrack>> GetAllTracks()
    //{
    //    var enabledSources = await GetEnabledSources();
    //    var tracks = await _library.GetAllTracks(enabledSources);
    //    return tracks.Select(t => new PlaylistTrackViewModel(t)).ToList();
    //}

    public async Task<ArtistViewModel> GetArtist(string artistId)
    {
        var enabledSources = await GetEnabledSources();

        var artist = await _library.GetAlbumArtist(artistId, enabledSources);

        var sorted = artist.Albums
            .Select(a => new AlbumViewModel(a))
            .OrderBy(a => a.Model.Album.ArtistName)
            .ThenBy(a => a.Model.Album.ReleaseType)
            .ThenBy(a => a.Model.Album.Year)
            .ToList();

        var groupedReleases = sorted.GroupBy(a => a.Model.Album.ReleaseType.GetGroup())
            .Select(r => new ReleaseGroup
            {
                Grouping = r.Key,
                Albums = r.ToList()
            })
            .ToList();

        return new ArtistViewModel
        {
            Artist = artist.Artist,
            Releases = groupedReleases
        };
    }

    //public async Task<TrackSummary> GetTrackSummary(LibrarySource source, string id)
    //{
    //    return await _library.GetTrackSummary(id, source);
    //}

    public async Task<List<LibrarySource>> GetEnabledSources()
    {
        var libraries = await _storeGetter.GetValues(StoreKey.Libraries);
        return libraries.Select(s => Enum.Parse<LibrarySource>(s)).ToList();
    }

    //public async Task<List<PlaylistTrackViewModel>> GetPlaylistTracks(string name)
    //{
    //    return new List<PlaylistTrackViewModel>();
    //}

    public async Task<TrackFull> GetTrack(LibrarySource source, string id)
    {
        return await _library.GetTrack(id, source);
    }

    public async Task<bool> UpdateAlbum(AlbumUpdate update)
    {
        var success = await _updater.UpdateAlbum(update);
        if (success)
        {
            AlbumUpdated?.Invoke(this, new AlbumUpdatedEventArgs(update.Item.Source, update));
        }
        return success;
    }

    public async Task<bool> UpdateArtist(ArtistUpdate update)
    {
        var success = await _updater.UpdateArtist(update);
        if (success)
        {
            ArtistUpdated?.Invoke(this, new ArtistUpdatedEventArgs(update));
        }
        return success;
        // Artist name not updated atm so don't need to include sidebar but will need to update:
        // Library Artist tab
        // Currently Playing tab if relevant
        // Library artist list - artist may have changed grouping
    }

    public async Task<bool> UpdateTrack(TrackUpdate update)
    {
        var success = await _updater.UpdateTrack(update);
        if (success)
        {
            TrackUpdated?.Invoke(this, new TrackUpdatedEventArgs(update));
        }
        return success;
    }
}