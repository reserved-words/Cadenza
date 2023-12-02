namespace Cadenza.Web.Core.Services;

internal class PlaylistCreator : IPlaylistCreator
{
    private readonly IAlbumApi _albumRepository;
    private readonly IArtistApi _artistRepository;
    private readonly IPlayApi _repository;
    private readonly ITrackApi _trackRepository;

    public PlaylistCreator(IPlayApi repository, IArtistApi artistRepository,
        IAlbumApi albumRepository, ITrackApi trackRepository)
    {
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

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks
        };
    }

    public async Task<PlaylistDefinition> CreateAlbumPlaylist(int id, int startTrackId)
    {
        var tracks = await _repository.PlayAlbum(id);
        var album = await _albumRepository.GetAlbum(id);

        var playlistId = new PlaylistId(id.ToString(), PlaylistType.Album, $"{album.Title} ({album.ArtistName})");

        var startTrackPosition = tracks.IndexOf(startTrackId);
        var startIndex = startTrackPosition >= 0 ? startTrackPosition : 0;

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

        var tracks = new List<int> { id };

        var playlistId = new PlaylistId(id.ToString(), PlaylistType.Track, $"{track.Track.Title} ({track.Artist.Name})");

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks
        };
    }

    public async Task<PlaylistDefinition> CreateLibraryPlaylist()
    {
        var tracks = await _repository.PlayAll();

        var playlistId = new PlaylistId("", PlaylistType.All, "All Library");

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks
        };
    }

    public async Task<PlaylistDefinition> CreateGroupingPlaylist(GroupingVM grouping)
    {
        var tracks = await _repository.PlayGrouping(grouping.Id);

        var playlistId = new PlaylistId(grouping.Id.ToString(), PlaylistType.Grouping, grouping.Name);

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks
        };
    }

    public async Task<PlaylistDefinition> CreateGenrePlaylist(string id)
    {
        var tracks = await _repository.PlayGenre(id);

        var playlistId = new PlaylistId(id, PlaylistType.Genre, id);

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks
        };
    }

    public async Task<PlaylistDefinition> CreateTagPlaylist(string id)
    {
        var tracks = await _repository.PlayTag(id);

        var playlistId = new PlaylistId(id, PlaylistType.Tag, id);

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks
        };
    }
}