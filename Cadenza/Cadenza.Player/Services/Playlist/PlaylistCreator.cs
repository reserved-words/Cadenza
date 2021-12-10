using Cadenza.Database;

namespace Cadenza.Player;

public class PlaylistCreator : IPlaylistCreator
{
    private readonly IShuffler _shuffler;
    private readonly IPlayTrackRepository _repository;

    public PlaylistCreator(IShuffler shuffler, IPlayTrackRepository repository)
    {
        _shuffler = shuffler;
        _repository = repository;
    }

    public async Task<PlaylistDefinition> CreateArtistPlaylist(LibraryArtist artist)
    {
        // this is the album tracks, probably need to change this to their actual tracks

        var tracks = await _repository.GetByArtist(artist.Id);

        var shuffledTracks = _shuffler.Shuffle(tracks).ToList();

        return new PlaylistDefinition
        {
            Type = PlaylistType.Artist,
            Name = artist.Name,
            Tracks = shuffledTracks,
            First = tracks.First()
        };
    }

    public async Task<PlaylistDefinition> CreateAlbumPlaylist(LibraryAlbum album)
    {
        var tracks = await _repository.GetByAlbum(album.Source, album.ArtistId, album.Id);

        return new PlaylistDefinition
        {
            Type = PlaylistType.Album,
            Name = $"{album.Title} by {album.Artist}",
            Tracks = tracks,
            First = tracks.First()
        };
    }

    public async Task<PlaylistDefinition> CreateLibraryPlaylist(string first = null)
    {
        //var ts = await _repository.GetAll();

        //var tracks = ts.Select(t => new PlaylistTrackViewModel(t));

        //var shuffledTracks = _shuffler.Shuffle(tracks).ToList();

        //var firstTrack = first != null
        //   ? tracks.SingleOrDefault(t => t.Model.Id == first)
        //   : null;

        //return new PlaylistDefinition
        //{
        //    Type = PlaylistType.All,
        //    Name = "All Library",
        //    Sections = shuffledTracks.Split(),
        //    First = firstTrack
        //};

        throw new NotImplementedException();
    }

    private Dictionary<int, List<PlayTrack>> GetAsDictionary(IEnumerable<PlayTrack> tracks)
    {
        return new Dictionary<int, List<PlayTrack>>
            {
                { 1, tracks.ToList() }
            };
    }
}