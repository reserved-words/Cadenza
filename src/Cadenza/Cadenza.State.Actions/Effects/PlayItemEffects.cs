namespace Cadenza.State.Actions.Effects;
internal class PlayItemEffects
{
    private readonly IPlaylistCreator _playlistCreator;

    public PlayItemEffects(IPlaylistCreator playlistCreator)
    {
        _playlistCreator = playlistCreator;
    }

    [EffectMethod]
    public async Task HandlePlayAlbumAction(PlayAlbumRequest action, IDispatcher dispatcher)
    {
        var playlist = await _playlistCreator.CreateAlbumPlaylist(action.Id, action.StartTrackId);
        dispatcher.Dispatch(new PlaylistStartRequest(playlist));
    }

    [EffectMethod]
    public async Task HandlePlayArtistAction(PlayArtistRequest action, IDispatcher dispatcher)
    {
        var playlist = await _playlistCreator.CreateArtistPlaylist(action.Id);
        dispatcher.Dispatch(new PlaylistStartRequest(playlist));
    }

    [EffectMethod]
    public async Task HandlePlayGenreAction(PlayGenreRequest action, IDispatcher dispatcher)
    {
        var playlist = await _playlistCreator.CreateGenrePlaylist(action.Id);
        dispatcher.Dispatch(new PlaylistStartRequest(playlist));
    }

    [EffectMethod]
    public async Task HandlePlayGroupingAction(PlayGroupingRequest action, IDispatcher dispatcher)
    {
        var playlist = await _playlistCreator.CreateGroupingPlaylist(action.Grouping);
        dispatcher.Dispatch(new PlaylistStartRequest(playlist));
    }

    [EffectMethod]
    public async Task HandlePlayTagAction(PlayTagRequest action, IDispatcher dispatcher)
    {
        var playlist = await _playlistCreator.CreateTagPlaylist(action.Id);
        dispatcher.Dispatch(new PlaylistStartRequest(playlist));
    }

    [EffectMethod]
    public async Task HandlePlayTrackAction(PlayTrackRequest action, IDispatcher dispatcher)
    {
        var playlist = await _playlistCreator.CreateTrackPlaylist(action.Id);
        dispatcher.Dispatch(new PlaylistStartRequest(playlist));
    }

    [EffectMethod]
    public async Task HandlePlayAllAction(PlayAllRequest action, IDispatcher dispatcher)
    {
        var playlist = await _playlistCreator.CreateLibraryPlaylist();
        dispatcher.Dispatch(new PlaylistStartRequest(playlist));
    }
}
