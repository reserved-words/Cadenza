//using Cadenza.Web.Common.Interfaces.Play;
//using Cadenza.Web.Common.Interfaces.Store;
//using Fluxor;

//namespace Cadenza.Web.Core.Coordinators;

//internal class PlayCoordinator : IPlayCoordinator
//{
//    private readonly ICurrentTrackStore _store;
//    private readonly IMessenger _messenger;
//    private readonly IDispatcher _dispatcher;

//    public PlayCoordinator(ICurrentTrackStore store, IMessenger messenger, IDispatcher dispatcher)
//    {
//        _store = store;
//        _messenger = messenger;

//        _messenger.Subscribe<TrackFinishedEventArgs>(OnTrackFinished);
//        _messenger.Subscribe<TrackRemovedEventArgs>(OnTrackRemoved);
//        _dispatcher = dispatcher;
//    }


//    public async Task StopCurrentPlaylist()
//    {
//        await StopTrack();
//        await StopPlaylist();
//        await _messenger.Send(this, new PlaylistLoadingEventArgs());
//    }

//    private async Task PlayTrack()
//    {
////        await _store.SetCurrentTrack(_currentPlaylist.Current.Id);

// //       _dispatcher.Dispatch(new FetchCurrentTrackAction())

//        await _messenger.Send(this, new StartTrackEventArgs
//        {
//            CurrentTrack = _currentPlaylist.Current,
//            IsLastTrack = _currentPlaylist.CurrentIsLast
//        });
//    }

//    private async Task StopTrack()
//    {
//        await _messenger.Send(this, new StopTrackEventArgs());
//    }

//    private async Task OnSkipNext (object sender, SkipNextTrackEventArgs args)
//    {
//        await StopTrack();

//        if (_currentPlaylist.CurrentIsLast)
//        {
//            await StopPlaylist();
//            return;
//        }

//        await _currentPlaylist.MoveNext();
//        await PlayTrack();
//    }

//    private async Task OnTrackFinished(object sender, TrackFinishedEventArgs args)
//    {
//        await OnSkipNext(this, null);
//    }

//    private Task OnTrackRemoved(object sender, TrackRemovedEventArgs args)
//    {
//        _currentPlaylist?.RemoveTrack(args.TrackId);
//        return Task.CompletedTask;
//    }

//    private async Task OnSkipPrevious(object sender, SkipPreviousTrackEventArgs args)
//    {
//        await StopTrack();
//        await _currentPlaylist.MovePrevious();
//        await PlayTrack();
//    }

//    private async Task StopPlaylist()
//    {
//        if (_currentPlaylist == null)
//            return;

//        await _messenger.Send(this, new PlaylistFinishedEventArgs { Playlist = _currentPlaylist.Id });
//        _currentPlaylist = null;
//    }
//}
