using Cadenza.Web.Common.Interfaces.Play;
using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Web.Core.Coordinators;

internal class PlayCoordinator : IPlayCoordinator
{
    private readonly ICurrentTrackStore _store;
    private readonly IMessenger _messenger;

    public PlayCoordinator(ICurrentTrackStore store, IMessenger messenger)
    {
        _store = store;
        _messenger = messenger;

        _messenger.Subscribe<SkipNextTrackEventArgs>(OnSkipNext);
        _messenger.Subscribe<SkipPreviousTrackEventArgs>(OnSkipPrevious);
        _messenger.Subscribe<TrackFinishedEventArgs>(OnTrackFinished);
    }

    private IPlaylist _currentPlaylist;

    public async Task Play(PlaylistDefinition playlistDefinition)
    {
        _currentPlaylist = new Playlist(playlistDefinition);

        for (var i = 0; i < playlistDefinition.StartIndex; i++)
        {
            await _currentPlaylist.MoveNext();
        }

        await _messenger.Send(this, new PlaylistStartedEventArgs { Playlist = _currentPlaylist.Id });
        await PlayTrack();
    }

    public async Task StopCurrentPlaylist()
    {
        await StopTrack();
        await StopPlaylist();
        await _messenger.Send(this, new PlaylistLoadingEventArgs());
    }

    private async Task PlayTrack()
    {
        await _store.SetCurrentTrack(_currentPlaylist.Current.Id);

        await _messenger.Send(this, new StartTrackEventArgs
        {
            CurrentTrack = _currentPlaylist.Current,
            IsLastTrack = _currentPlaylist.CurrentIsLast
        });
    }

    private async Task StopTrack()
    {
        await _messenger.Send(this, new StopTrackEventArgs());
    }

    private async Task OnSkipNext (object sender, SkipNextTrackEventArgs args)
    {
        await StopTrack();

        if (_currentPlaylist.CurrentIsLast)
        {
            await StopPlaylist();
            return;
        }

        await _currentPlaylist.MoveNext();
        await PlayTrack();
    }

    private async Task OnTrackFinished(object arg1, TrackFinishedEventArgs arg2)
    {
        await OnSkipNext(this, null);
    }

    private async Task OnSkipPrevious(object sender, SkipPreviousTrackEventArgs args)
    {
        await StopTrack();
        await _currentPlaylist.MovePrevious();
        await PlayTrack();
    }

    private async Task StopPlaylist()
    {
        if (_currentPlaylist == null)
            return;

        await _messenger.Send(this, new PlaylistFinishedEventArgs { Playlist = _currentPlaylist.Id });
        _currentPlaylist = null;
    }
}
