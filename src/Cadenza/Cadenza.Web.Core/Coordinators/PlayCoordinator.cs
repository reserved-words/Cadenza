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
        _messenger.Subscribe<TrackStatusEventArgs>(OnTrackStatusChanged);
    }

    private IPlaylist _currentPlaylist;

    public async Task Play(PlaylistDefinition playlistDefinition)
    {
        _currentPlaylist = new Playlist(playlistDefinition);
        await PlayTrack();
        await _messenger.Send(this, new PlaylistStartedEventArgs { Playlist = _currentPlaylist.Id });
    }

    public async Task LoadingPlaylist()
    {
        await StopPlaylist();
        await _messenger.Send(this, new PlaylistLoadingEventArgs());
    }

    private async Task PlayTrack()
    {
        if (_currentPlaylist.Current == null)
            return;

        var currentTrack = await _repository.GetTrack(_currentPlaylist.Current.Id);
        await _store.SetValue(StoreKey.CurrentTrack, currentTrack);
        await _store.SetValue(StoreKey.CurrentTrackSource, currentTrack.Track.Source);

        await _messenger.Send(this, new StartTrackEventArgs
        {
            CurrentTrack = _currentPlaylist.Current,
            IsLastTrack = _currentPlaylist.CurrentIsLast
        });
    }

    private async Task OnSkipNext (object sender, SkipNextTrackEventArgs args)
    {
        if (_currentPlaylist.CurrentIsLast)
        {
            await StopPlaylist();
            return;
        }

        await _currentPlaylist.MoveNext();
        await PlayTrack();
    }

    private async Task OnSkipPrevious(object sender, SkipPreviousTrackEventArgs args)
    {
        await _currentPlaylist.MovePrevious();
        await PlayTrack();
    }

    private async Task StopPlaylist()
    {
        if (_currentPlaylist != null)
        {
            await _messenger.Send(this, new PlaylistFinishedEventArgs { Playlist = _currentPlaylist.Id });
            _currentPlaylist = null;
        }
    }

    private async Task OnTrackStatusChanged(object sender, TrackStatusEventArgs args)
    {
        if (args.Status == PlayStatus.Stopped && args.Track != null)
        {
            await OnSkipNext(this, null);
        }
    }
}
