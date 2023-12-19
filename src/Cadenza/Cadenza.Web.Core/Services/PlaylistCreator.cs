using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Core.Services;

internal class PlaylistCreator : IPlaylistCreator
{
    private readonly IPlayApi _playApi;

    public PlaylistCreator(IPlayApi playApi)
    {
        _playApi = playApi;
    }

    public async Task<PlaylistDefinition> CreateArtistPlaylist(int id)
    {
        var playlist = await _playApi.PlayArtist(id);

        var playlistId = new PlaylistId(playlist.Id, PlaylistType.Artist, playlist.Title);

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = playlist.TrackIds
        };
    }

    public async Task<PlaylistDefinition> CreateAlbumPlaylist(int id, int startTrackId)
    {
        var playlist = await _playApi.PlayAlbum(id);
        return GetDefinition(PlaylistType.Album, playlist, startTrackId);
    }

    public async Task<PlaylistDefinition> CreateTrackPlaylist(int id)
    {
        var playlist = await _playApi.PlayTrack(id);
        return GetDefinition(PlaylistType.Track, playlist);
    }

    public async Task<PlaylistDefinition> CreateLibraryPlaylist()
    {
        var playlist = await _playApi.PlayAll();
        return GetDefinition(PlaylistType.All, playlist);
    }

    public async Task<PlaylistDefinition> CreateGroupingPlaylist(string grouping)
    {
        var playlist = await _playApi.PlayGrouping(grouping);
        return GetDefinition(PlaylistType.Grouping, playlist);
    }

    public async Task<PlaylistDefinition> CreateGenrePlaylist(string genre)
    {
        var playlist = await _playApi.PlayGenre(genre);
        return GetDefinition(PlaylistType.Genre, playlist);
    }

    public async Task<PlaylistDefinition> CreateTagPlaylist(string id)
    {
        var playlist = await _playApi.PlayTag(id);
        return GetDefinition(PlaylistType.Tag, playlist);
    }

    private PlaylistDefinition GetDefinition(PlaylistType type, PlaylistVM playlist, int? startTrackId = null)
    {
        var playlistId = new PlaylistId(playlist.Id, type, playlist.Title);

        var startIndex = startTrackId.HasValue
            ? GetStartIndex(playlist, startTrackId.Value)
            : 0;

        return new PlaylistDefinition
        {
            Id = playlistId,
            Tracks = playlist.TrackIds,
            StartIndex = startIndex
        };
    }

    private static int GetStartIndex(PlaylistVM playlist, int startTrackId)
    {
        var startTrackPosition = playlist.TrackIds.IndexOf(startTrackId);
        return startTrackPosition >= 0 ? startTrackPosition : 0;
    }
}