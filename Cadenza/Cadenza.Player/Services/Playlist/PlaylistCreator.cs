namespace Cadenza.Player;

public class PlaylistCreator : IPlaylistCreator
{
    private readonly IViewModelLibrary _library;
    private readonly IShuffler _shuffler;

    public PlaylistCreator(IShuffler shuffler, IViewModelLibrary library)
    {
        _shuffler = shuffler;
        _library = library;
    }

    public async Task<PlaylistDefinition> CreateArtistPlaylist(string artistId, string first = null)
    {
        // this is the album tracks, probably need to change this to their actual tracks

        var artist = await _library.GetArtist(artistId);

        var tracks = artist.Releases
            .SelectMany(r => r.Albums)
            .SelectMany(a => a.Model.Discs)
            .SelectMany(a => a.Tracks)
            .Select(t => new PlaylistTrackViewModel(t.Track));

        var shuffledTracks = _shuffler.Shuffle(tracks).ToList();

        var firstTrack = first != null
            ? shuffledTracks.SingleOrDefault(t => t.Model.Id == first)
            : null;

        return new PlaylistDefinition
        {
            Type = PlaylistType.Artist,
            Name = artist.Artist.Name,
            Sections = shuffledTracks.Split(),
            First = firstTrack
        };
    }

    public async Task<PlaylistDefinition> CreateAlbumPlaylist(AlbumFull album)
    {
        var sections = album.Discs.Split(
            d => d.Name,
            d => d.Tracks
                .OrderBy(t => t.Position.TrackNo)
                .Select(t => new PlaylistTrackViewModel(t.Track))
                .ToList());

        return new PlaylistDefinition
        {
            Type = PlaylistType.Album,
            Name = $"{album.Album.Title} by {album.Album.ArtistName}",
            Sections = sections,
            First = sections.First
        };
    }

    public async Task<PlaylistDefinition> CreateLibraryPlaylist(string first = null)
    {
        var tracks = await _library.GetAllTracks();

        var shuffledTracks = _shuffler.Shuffle(tracks).ToList();

        var firstTrack = first != null
           ? tracks.SingleOrDefault(t => t.Model.Id == first)
           : null;

        return new PlaylistDefinition
        {
            Type = PlaylistType.All,
            Name = "All Library",
            Sections = shuffledTracks.Split(),
            First = firstTrack
        };
    }

    private Dictionary<int, List<PlaylistTrackViewModel>> GetAsDictionary(IEnumerable<PlaylistTrackViewModel> tracks)
    {
        return new Dictionary<int, List<PlaylistTrackViewModel>>
            {
                { 1, tracks.ToList() }
            };
    }
}