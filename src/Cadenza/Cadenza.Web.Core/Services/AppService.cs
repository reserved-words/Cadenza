namespace Cadenza.Web.Core.Services;

internal class AppService : IAppConsumer, IAppController
{
    public event TrackStatusEventHandler TrackStatusChanged;
    public event TrackEventHandler StartTrack;

    public event PlaylistEventHandler PlaylistFinished;
    public event PlaylistEventHandler PlaylistLoading;
    public event PlaylistEventHandler PlaylistStarted;

    public event ItemEventHandler ItemRequested;

    private IPlaylist _currentPlaylist;

    public async Task Play(PlaylistDefinition playlistDefinition)
    {
        _currentPlaylist = new Playlist(playlistDefinition);
        await PlayTrack();
        await PlaylistStarted?.Invoke(this, GetPlaylistArgs());
    }

    private async Task PlayTrack()
    {
        if (_currentPlaylist.Current == null)
            return;

        await StartTrack?.Invoke(this, new TrackEventArgs
        {
            CurrentTrack = _currentPlaylist.Current,
            IsLastTrack = _currentPlaylist.CurrentIsLast
        });
    }

    public async Task SkipNext()
    {
        if (_currentPlaylist.CurrentIsLast)
        {
            await StopPlaylist();
            return;
        }

        await _currentPlaylist.MoveNext();
        await PlayTrack();
    }

    public async Task SkipPrevious()
    {
        await _currentPlaylist.MovePrevious();
        await PlayTrack();
    }

    private PlaylistEventArgs GetPlaylistArgs(string error = null)
    {
        return new PlaylistEventArgs
        {
            Playlist = _currentPlaylist.Id,
            Error = error
        };
    }

    public async Task LoadingPlaylist()
    {
        await StopPlaylist();
        await PlaylistLoading?.Invoke(this, new PlaylistEventArgs());
    }

    private async Task StopPlaylist()
    {
        await StartTrack?.Invoke(this, new TrackEventArgs
        {
            CurrentTrack = null,
            IsLastTrack = false
        });

        if (_currentPlaylist != null)
        {
            await PlaylistFinished?.Invoke(this, GetPlaylistArgs());
            _currentPlaylist = null;
        }
    }

    public async Task View(ViewItem item)
    {
        await ItemRequested?.Invoke(this, new ItemEventArgs { Item = item });
    }

    public async Task OnTrackStatusChanged(TrackStatusEventArgs args)
    {
        if (TrackStatusChanged != null)
        {
            await TrackStatusChanged.Invoke(this, args);
        }

        if (args.Status == PlayStatus.Stopped && args.Track != null)
        {
            await SkipNext();
        }
    }
}