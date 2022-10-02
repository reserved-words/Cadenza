using Cadenza.Web.Common.Interfaces.Store;
using Cadenza.Web.Player.Interfaces;

namespace Cadenza.Web.Player;

public class PlayerBase : ComponentBase
{
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
    }

    protected async Task Pause()
    {
        await Player.Pause();
        await OnStatusChanged(PlayStatus.Paused);
    }

    protected async Task Resume()
    {
        await Player.Resume();
        await OnStatusChanged(PlayStatus.Playing);
    }

    private async Task OnStatusChanged(PlayStatus status)
    {
        await Messenger.Send(this, new PlayStatusEventArgs { Track = Track, Status = status });
    }

    private async Task OnStartTrack(object sender, StartTrackEventArgs e)
    {
        Track = e.CurrentTrack;
        Loading = false;
        IsLastTrack = e.IsLastTrack;
        await Player.Play(Track);
        Model = await Store.GetCurrentTrack();
        StateHasChanged();
        await OnStatusChanged(PlayStatus.Playing);
    }

    private async Task OnStopTrack(object sender, StopTrackEventArgs e)
    {
        if (Track == null)
            return;

        Track = null;
        Model = null;
        StateHasChanged();
        await Player.Stop();
        await OnStatusChanged(PlayStatus.Stopped);
    }
}
