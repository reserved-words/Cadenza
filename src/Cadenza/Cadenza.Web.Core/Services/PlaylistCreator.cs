namespace Cadenza.Web.Core.Services;

internal class PlaylistCreator : IPlaylistCreator
{
    private readonly IShuffler _shuffler;
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;
    private readonly IPlayTrackRepository _repository;
    private readonly ITrackRepository _trackRepository;

    public PlaylistCreator(IShuffler shuffler, IPlayTrackRepository repository, IArtistRepository artistRepository,
        IAlbumRepository albumRepository, ITrackRepository trackRepository)
    {
        _shuffler = shuffler;
        _repository = repository;
        _artistRepository = artistRepository;
        _albumRepository = albumRepository;
        _trackRepository = trackRepository;
    }

    public async Task<PlaylistDefinition> CreateArtistPlaylist(int id)
    {
        var artist = await _artistRepository.GetArtist(id);
        var tracks = await _repository.PlayArtist(id);

        var playlistId = new PlaylistId(id.ToString(), PlaylistType.Artist, artist.Name);

        var shuffledTracks = _shuffler.Shuffle(tracks.ToList());

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = shuffledTracks.ToList()
        };
    }

    public async Task<PlaylistDefinition> CreateAlbumPlaylist(int id, int startTrackId)
    {
        var tracks = await _repository.PlayAlbum(id);
        var album = await _albumRepository.GetAlbum(id);

        var playlistId = new PlaylistId(id.ToString(), PlaylistType.Album, $"{album.Title} ({album.ArtistName})");

        var startTrack = tracks.SingleOrDefault(t => t.Id == startTrackId);
        var startIndex = startTrack != null ? tracks.IndexOf(startTrack) : 0;

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks.ToList(),
            StartIndex = startIndex
        };
    }

    public async Task<PlaylistDefinition> CreateTrackPlaylist(int id)
    {
        var track = await _trackRepository.GetTrack(id);

        var playTrack = new PlayTrack
        {
            Id = id,
            IdFromSource = track.Track.IdFromSource,
            Source = track.Track.Source,
            ArtistId = track.Artist.Id,
            AlbumId = track.Album.Id,
            Title = track.Track.Title
        };

        var tracks = new List<PlayTrack> { playTrack };

        var playlistId = new PlaylistId(id.ToString(), PlaylistType.Track, $"{track.Track.Title} ({track.Artist.Name})");

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks
        };
    }

    public async Task<PlaylistDefinition> CreateLibraryPlaylist(string first = null)
    {
        var tracks = await _repository.PlayAll();

        var shuffledTracks = _shuffler.Shuffle(tracks.ToList());

        var playlistId = new PlaylistId("", PlaylistType.All, "All Library");

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = shuffledTracks.ToList()
        };
    }

    public async Task<PlaylistDefinition> CreateGroupingPlaylist(Grouping id)
    {
        var tracks = await _repository.PlayGrouping(id);

        var playlistId = new PlaylistId(id.ToString(), PlaylistType.Grouping, id.GetDisplayName());

        var shuffledTracks = _shuffler.Shuffle(tracks.ToList());

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = shuffledTracks.ToList()
        };
    }

    public async Task<PlaylistDefinition> CreateGenrePlaylist(string id)
    {
        var tracks = await _repository.PlayGenre(id);

        var playlistId = new PlaylistId(id, PlaylistType.Genre, id);

        var shuffledTracks = _shuffler.Shuffle(tracks.ToList());

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = shuffledTracks.ToList()
        };
    }

    public async Task<PlaylistDefinition> CreateTagPlaylist(string id)
    {
        var tracks = await _repository.PlayTag(id);

        var playlistId = new PlaylistId(id, PlaylistType.Tag, id);

        var shuffledTracks = _shuffler.Shuffle(tracks.ToList());

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = shuffledTracks.ToList()
        };
    }
}