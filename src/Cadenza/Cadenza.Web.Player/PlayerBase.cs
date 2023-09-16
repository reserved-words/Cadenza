using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.Web.Player;

public class PlayerBase : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; }

    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public ICurrentTrackStore Store { get; set; }

    [Inject]
    internal IPlayer Player { get; set; }

    public bool Loading { get; set; }

    protected PlayTrack Track { get; set; }

    protected bool IsLastTrack { get; set; }

    protected TrackFull Model { get; set; }

    protected bool Empty => Model == null && !Loading;

    protected override void OnInitialized()
    {
        Messenger.Subscribe<StartTrackEventArgs>(OnStartTrack);
        Messenger.Subscribe<StopTrackEventArgs>(OnStopTrack);
        Messenger.Subscribe<PlaylistFinishedEventArgs>(OnPlaylistFinished);
    }

    protected async Task Pause()
    {
        await Player.Pause();
        //await OnStatusChanged(PlayStatus.Paused);
        Dispatcher.Dispatch(new PlayStatusPausedAction());
    }

    protected async Task Resume()
    {
        await Player.Resume();
        //await OnStatusChanged(PlayStatus.Playing);
        Dispatcher.Dispatch(new PlayStatusResumedAction());
    }

    private async Task OnStartTrack(object sender, StartTrackEventArgs e)
    {
        Model = await Store.GetCurrentTrack();
        Track = e.CurrentTrack;
        Loading = false;
        IsLastTrack = e.IsLastTrack;
        StateHasChanged();
        await Player.Play(Track);
        //await OnStatusChanged(PlayStatus.Playing);
        Dispatcher.Dispatch(new PlayStatusPlayingAction(Track));
    }

    private async Task OnStopTrack(object sender, StopTrackEventArgs e)
    {
        if (Track == null)
            return;

        Track = null;
        Loading = true;
        StateHasChanged();
        await Player.Stop();
        //await OnStatusChanged(PlayStatus.Stopped);
        Dispatcher.Dispatch(new PlayStatusStoppedAction());
    }

    //private async Task OnStatusChanged(PlayStatus status)
    //{
        // await Messenger.Send(this, new PlayStatusEventArgs { Track = Track, Status = status });
    //}

    private Task OnPlaylistFinished(object arg1, PlaylistFinishedEventArgs arg2)
    {
        Model = null;
        Loading = false;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
