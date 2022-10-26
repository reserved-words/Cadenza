using Cadenza.Web.Common.Interfaces.Play;

namespace Cadenza.Web.Core.Services;

internal class ItemPlayer : IItemPlayer
{
    private readonly IPlaylistCreator _playlistCreator;
    private readonly IPlayCoordinator _playCoordinator;

    public ItemPlayer(IPlayCoordinator playCoordinator, IPlaylistCreator playlistCreator)
    {
        _playCoordinator = playCoordinator;
        _playlistCreator = playlistCreator;
    }

    public async Task PlayAlbum(string id)
    {
        await _playCoordinator.StopCurrentPlaylist();
        var playlist = await _playlistCreator.CreateAlbumPlaylist(id);
        await _playCoordinator.Play(playlist);
    }

    public async Task PlayArtist(string id)
    {
        await _playCoordinator.StopCurrentPlaylist();
        var playlist = await _playlistCreator.CreateArtistPlaylist(id);
        await _playCoordinator.Play(playlist);
    }

    public async Task PlayGenre(string id)
    {
        await _playCoordinator.StopCurrentPlaylist();
        var playlist = await _playlistCreator.CreateGenrePlaylist(id);
        await _playCoordinator.Play(playlist);
    }

    public async Task PlayGrouping(Grouping id)
    {
        await _playCoordinator.StopCurrentPlaylist();
        var playlist = await _playlistCreator.CreateGroupingPlaylist(id);
        await _playCoordinator.Play(playlist);
    }

    public async Task PlayTag(string id)
    {
        await _playCoordinator.StopCurrentPlaylist();
        var playlist = await _playlistCreator.CreateTagPlaylist(id);
        await _playCoordinator.Play(playlist);
    }

    public async Task PlayTrack(string id)
    {
        await _playCoordinator.StopCurrentPlaylist();
        var playlist = await _playlistCreator.CreateTrackPlaylist(id);
        await _playCoordinator.Play(playlist);
    }

    public async Task PlayAll()
    {
        await _playCoordinator.StopCurrentPlaylist();
        var playlist = await _playlistCreator.CreateLibraryPlaylist();
        await _playCoordinator.Play(playlist);
    }

    public async Task PlayItem(PlayerItemType type, string id)
    {
        switch (type)
        {
            case PlayerItemType.Album:
                await PlayAlbum(id);
                break;
            case PlayerItemType.Artist:
                await PlayArtist(id);
                break;
            case PlayerItemType.Genre:
                await PlayGenre(id);
                break;
            case PlayerItemType.Grouping:
                await PlayGrouping(id.Parse<Grouping>());
                break;
            case PlayerItemType.Tag:
                await PlayTag(id);
                break;
            case PlayerItemType.Track:
                await PlayTrack(id);
                break;
        }

    }
}