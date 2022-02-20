namespace Cadenza.Core;

public class ItemPlayer : IItemPlayer
{
    private readonly IPlaylistCreator _playlistCreator;
    private readonly IAppController _app;

    public ItemPlayer(IAppController app, IPlaylistCreator playlistCreator)
    {
        _app = app;
        _playlistCreator = playlistCreator;
    }

    public async Task PlayAlbum(LibrarySource source, string id)
    {
        await _app.LoadingPlaylist();
        var playlist = await _playlistCreator.CreateAlbumPlaylist(source, id);
        await _app.Play(playlist);
    }

    public async Task PlayArtist(string id)
    {
        await _app.LoadingPlaylist();
        var playlist = await _playlistCreator.CreateArtistPlaylist(id);
        await _app.Play(playlist);
    }

    public async Task PlayPlaylist(string id)
    {
        throw new NotImplementedException();
    }

    public async Task PlayTrack(LibrarySource source, string id)
    {
        await _app.LoadingPlaylist();
        var playlist = await _playlistCreator.CreateTrackPlaylist(source, id);
        await _app.Play(playlist);
    }

    public async Task PlayAll()
    {
        await _app.LoadingPlaylist();
        var playlist = await _playlistCreator.CreateLibraryPlaylist();
        await _app.Play(playlist);
    }
}