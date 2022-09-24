using Cadenza.Domain.Enums;
using Cadenza.Web.Core.Interfaces;
using Cadenza.Web.Core.Playlists;

namespace Cadenza.Web.Core.App;

public class ItemPlayer : IItemPlayer
{
    private readonly IPlaylistCreator _playlistCreator;
    private readonly IAppController _app;

    public ItemPlayer(IAppController app, IPlaylistCreator playlistCreator)
    {
        _app = app;
        _playlistCreator = playlistCreator;
    }

    public async Task PlayAlbum(string id)
    {
        await _app.LoadingPlaylist();
        var playlist = await _playlistCreator.CreateAlbumPlaylist(id);
        await _app.Play(playlist);
    }

    public async Task PlayArtist(string id)
    {
        await _app.LoadingPlaylist();
        var playlist = await _playlistCreator.CreateArtistPlaylist(id);
        await _app.Play(playlist);
    }

    public async Task PlayGrouping(Grouping id)
    {
        await _app.LoadingPlaylist();
        var playlist = await _playlistCreator.CreateGroupingPlaylist(id);
        await _app.Play(playlist);
    }

    public async Task PlayGenre(string id)
    {
        await _app.LoadingPlaylist();
        var playlist = await _playlistCreator.CreateGenrePlaylist(id);
        await _app.Play(playlist);
    }

    public Task PlayPlaylist(string id)
    {
        throw new NotImplementedException();
    }

    public async Task PlayTrack(string id)
    {
        await _app.LoadingPlaylist();
        var playlist = await _playlistCreator.CreateTrackPlaylist(id);
        await _app.Play(playlist);
    }

    public async Task PlayAll()
    {
        await _app.LoadingPlaylist();
        var playlist = await _playlistCreator.CreateLibraryPlaylist();
        await _app.Play(playlist);
    }
}