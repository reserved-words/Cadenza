using Cadenza.Web.Common.Interfaces.Play;

namespace Cadenza.Web.Core.Services;

internal class ItemPlayer : IItemPlayer
{
    private readonly IPlaylistCreator _playlistCreator;
    private readonly IPlayCoordinator _playCoordinator;
    private readonly IHistoryLogger _historyLogger;

    public ItemPlayer(IPlayCoordinator playCoordinator, IPlaylistCreator playlistCreator, IHistoryLogger historyLogger)
    {
        _playCoordinator = playCoordinator;
        _playlistCreator = playlistCreator;
        _historyLogger = historyLogger;
    }

    public async Task PlayAlbum(int id, int startTrackId = 0)
    {
        var playlist = await _playlistCreator.CreateAlbumPlaylist(id, startTrackId);
        await Play(playlist);
    }

    public async Task PlayArtist(int id)
    {
        var playlist = await _playlistCreator.CreateArtistPlaylist(id);
        await Play(playlist);
    }

    public async Task PlayGenre(string id)
    {
        var playlist = await _playlistCreator.CreateGenrePlaylist(id);
        await Play(playlist);
    }

    public async Task PlayGrouping(Grouping grouping)
    {
        var playlist = await _playlistCreator.CreateGroupingPlaylist(grouping);
        await Play(playlist);
    }

    public async Task PlayTag(string id)
    {
        var playlist = await _playlistCreator.CreateTagPlaylist(id);
        await Play(playlist);
    }

    public async Task PlayTrack(int id)
    {
        var playlist = await _playlistCreator.CreateTrackPlaylist(id);
        await Play(playlist);
    }

    public async Task PlayAll()
    {
        var playlist = await _playlistCreator.CreateLibraryPlaylist();
        await Play(playlist);
    }

    private async Task Play(PlaylistDefinition playlist)
    {
        await _playCoordinator.StopCurrentPlaylist();
        await _playCoordinator.Play(playlist);
        await _historyLogger.LogPlayedItem(playlist.Id);
    }
}