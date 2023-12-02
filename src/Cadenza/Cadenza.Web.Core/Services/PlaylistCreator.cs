using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Core.Services;

internal class PlaylistCreator : IPlaylistCreator
{
    private readonly ILibraryApi _libraryApi;
    private readonly IPlayApi _playApi;

    public PlaylistCreator(IPlayApi playApi, ILibraryApi libraryApi)
    {
        _playApi = playApi;
        _libraryApi = libraryApi;
    }

    public async Task<PlaylistDefinition> CreateArtistPlaylist(int id)
    {
        var artist = await _libraryApi.GetArtist(id);
        var tracks = await _playApi.PlayArtist(id);

        var playlistId = new PlaylistId(id.ToString(), PlaylistType.Artist, artist.Name);

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks
        };
    }

    public async Task<PlaylistDefinition> CreateAlbumPlaylist(int id, int startTrackId)
    {
        var tracks = await _playApi.PlayAlbum(id);
        var album = await _libraryApi.GetAlbum(id);

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
        var track = await _libraryApi.GetTrack(id);

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
        var tracks = await _playApi.PlayAll();

        var playlistId = new PlaylistId("", PlaylistType.All, "All Library");

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks
        };
    }

    public async Task<PlaylistDefinition> CreateGroupingPlaylist(GroupingVM grouping)
    {
        var tracks = await _playApi.PlayGrouping(grouping.Id);

        var playlistId = new PlaylistId(grouping.Id.ToString(), PlaylistType.Grouping, grouping.Name);

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks
        };
    }

    public async Task<PlaylistDefinition> CreateGenrePlaylist(string id)
    {
        var tracks = await _playApi.PlayGenre(id);

        var playlistId = new PlaylistId(id, PlaylistType.Genre, id);

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks
        };
    }

    public async Task<PlaylistDefinition> CreateTagPlaylist(string id)
    {
        var tracks = await _playApi.PlayTag(id);

        var playlistId = new PlaylistId(id, PlaylistType.Tag, id);

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = tracks
        };
    }
}