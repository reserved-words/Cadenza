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

    public async Task<PlaylistDefinition> CreateArtistPlaylist(string id)
    {
        var artist = await _artistRepository.GetArtist(id);
        var tracks = await _repository.GetByArtist(id);

        var firstSource = tracks.First().Source;

        LibrarySource? source = tracks.All(t => t.Source == firstSource)
            ? firstSource
            : null;

        var playlistId = new PlaylistId(id, source, PlaylistType.Artist, artist.Name);

        var shuffledTracks = _shuffler.Shuffle(tracks.ToList());

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = shuffledTracks.ToList()
        };
    }

    public async Task<PlaylistDefinition> CreateAlbumPlaylist(string id)
    {
        var tracks = await _repository.GetByAlbum(id);
        var album = await _albumRepository.GetAlbum(id);

        var playlistId = new PlaylistId(id, album.Source, PlaylistType.Album, $"{album.Title} ({album.ArtistName})");

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks.ToList()
        };
    }

    public async Task<PlaylistDefinition> CreateTrackPlaylist(string id)
    {
        var track = await _trackRepository.GetTrack(id);

        var playTrack = new PlayTrack
        {
            Id = id,
            Source = track.Track.Source,
            ArtistId = track.Artist.Id,
            AlbumId = track.Album.Id,
            Title = track.Track.Title
        };

        var tracks = new List<PlayTrack> { playTrack };

        var playlistId = new PlaylistId(id, playTrack.Source, PlaylistType.Track, $"{track.Track.Title} ({track.Artist.Name})");

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks
        };
    }

    public async Task<PlaylistDefinition> CreateLibraryPlaylist(string first = null)
    {
        var tracks = await _repository.GetAll();

        var shuffledTracks = _shuffler.Shuffle(tracks.ToList());

        var playlistId = new PlaylistId("", null, PlaylistType.All, "All Library");

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = shuffledTracks.ToList()
        };
    }

    public async Task<PlaylistDefinition> CreateGroupingPlaylist(Grouping id)
    {
        var tracks = await _repository.GetByGrouping(id);

        var playlistId = new PlaylistId(id.ToString(), null, PlaylistType.Grouping, id.GetDisplayName());

        var shuffledTracks = _shuffler.Shuffle(tracks.ToList());

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = shuffledTracks.ToList()
        };
    }

    public async Task<PlaylistDefinition> CreateGenrePlaylist(string id)
    {
        var tracks = await _repository.GetByGenre(id);

        var playlistId = new PlaylistId(id.ToString(), null, PlaylistType.Genre, id);

        var shuffledTracks = _shuffler.Shuffle(tracks.ToList());

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = shuffledTracks.ToList()
        };
    }
}