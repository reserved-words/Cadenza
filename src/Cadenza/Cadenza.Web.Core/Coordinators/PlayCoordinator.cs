using Cadenza.Web.Common.Interfaces.Play;
using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Web.Core.Coordinators;

internal class PlayCoordinator : IPlayCoordinator
{
    private readonly IAppStore _store;
    private readonly ITrackRepository _repository;
    private readonly IMessenger _messenger;

    public PlayCoordinator(IAppStore appStore, ITrackRepository repository, IMessenger messenger)
    {
        _store = appStore;
        _repository = repository;
        _messenger = messenger;

        _messenger.Subscribe<SkipNextTrackEventArgs>(OnSkipNext);
        _messenger.Subscribe<SkipPreviousTrackEventArgs>(OnSkipPrevious);
        _messenger.Subscribe<TrackFinishedEventArgs>(OnTrackFinished);
    }

    private IPlaylist _currentPlaylist;

    public async Task Play(PlaylistDefinition playlistDefinition)
    {
        _currentPlaylist = new Playlist(playlistDefinition);
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
        await StoreCurrentTrack();

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

    private async Task StoreCurrentTrack()
    {
        var currentTrack = await _repository.GetTrack(_currentPlaylist.Current.Id);
        await _store.SetValue(StoreKey.CurrentTrack, currentTrack);
        await _store.SetValue(StoreKey.CurrentTrackSource, currentTrack.Track.Source);
    }

    private async Task OnSkipNext (object sender, SkipNextTrackEventArgs args)
    {
        await StopTrack();

        if (_currentPlaylist.CurrentIsLast)
        {
            _currentPlaylist = null;
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
