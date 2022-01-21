//namespace Cadenza.Source.Spotify;

//public class SpotifyRepository : ISourceRepository
//{
//    private ILibrary _library;

//    public SpotifyRepository(SpotifyOverridesLibrary overrides, SpotifyApiLibrary api)
//    {
//        _overrides = overrides;
//        _api = api;
//    }

//    public LibrarySource Source => LibrarySource.Spotify;

//    public async Task<ICollection<ArtistInfo>> GetArtists()
//    {
//        await PopulateLibrary();
//        return _library.Artists;
//    }

//    public async Task<ICollection<AlbumInfo>> GetAlbums()
//    {
//        await PopulateLibrary();
//        return _library.Albums;
//    }

//    public async Task<TrackSummary> GetTrack(string id)
//    {
//        await PopulateLibrary();
//        var track = _library.Tracks.Single(t => t.Id == id);
//        return new TrackSummary
//        {
//            Id = track.Id,
//            // TODO
//        };
//    }

//    public async Task<TrackFull> GetTrackFull(string id)
//    {
//        await PopulateLibrary();
//        var track = _library.Tracks.Single(t => t.Id == id);
//        return new TrackFull
//        {
//            Id = track.Id,
//            // TODO
//        };
//    }

//    public async Task<List<string>> GetAlbumTracks(string artistId, string albumId)
//    {
//        await PopulateLibrary();
//        return _library.AlbumTrackLinks
//            .Where(t => t.AlbumId == albumId)
//            .Select(t => t.TrackId)
//            .ToList();
//    }

//    public async Task<List<string>> GetArtistTracks(string id)
//    {
//        await PopulateLibrary();
//        return _library.Tracks
//            .Where(t => t.ArtistId == id)
//            .Select(t => t.Id)
//            .ToList();
//    }

//    public async Task<List<string>> GetAllTracks()
//    {
//        await PopulateLibrary();
//        return _library.Tracks.Select(t => t.Id).ToList();
//    }

//    private async Task PopulateLibrary()
//    {
//        if (_library != null)
//            return;

//        var getSpotify = _api.GetStaticLibrary();
//        var getOverrides = _overrides.GetStaticLibrary();

//        await Task.WhenAll(getSpotify, getOverrides)
//            .ContinueWith(tasks =>
//            {
//                // add error handling
//                _library = getSpotify.Result;
                
//            });
//    }

//    private class Identifier
//    {
//        public ItemType Type { get; set; }
//        public string Id { get; set; }

//        public override bool Equals(object obj)
//        {
//            if (!(obj is Identifier identifier))
//                return false;

//            return identifier.Type == Type && identifier.Id == Id;
//        }

//        public override int GetHashCode()
//        {
//            return Type.GetHashCode() ^ Id.GetHashCode();
//        }
//    }

//}

