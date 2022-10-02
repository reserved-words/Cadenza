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

    public PlayTrack Track { get; set; }

    public bool IsLastTrack { get; set; }

    public TrackFull Model { get; set; }

    public bool CanPause { get; set; }

    public bool CanPlay { get; set; }

    public bool CanSkipNext { get; set; }

    public bool Empty => Model == null && !Loading;

    protected override void OnInitialized()
    {
        Messenger.Subscribe<StartTrackEventArgs>(OnStartTrack);
        Messenger.Subscribe<StopTrackEventArgs>(OnStopTrack);
    }

    public async Task Pause()
    {
        UpdatePlayState(true, false);
        await Player.Pause();
        await OnStatusChanged(PlayStatus.Paused);
    }

    public async Task Resume()
    {
        UpdatePlayState(false, true);
        await Player.Resume();
        await OnStatusChanged(PlayStatus.Playing);
    }

    private async Task OnStatusChanged(PlayStatus status)
    {
        await Messenger.Send(this, new PlayStatusEventArgs { Track = Track, Status = status });
    }

    private void UpdatePlayState(bool canPlay, bool canPause)
    {
        CanPlay = canPlay;
        CanPause = canPause;
        CanSkipNext = (canPlay || canPause) && !IsLastTrack; 
        StateHasChanged();
    }

    private async Task OnStartTrack(object sender, StartTrackEventArgs e)
    {
        Track = e.CurrentTrack;
        Loading = false;
        IsLastTrack = e.IsLastTrack;
        Model = await Store.GetCurrentTrack();
        await Player.Play(Track);
        UpdatePlayState(false, true);
        StateHasChanged();
        await OnStatusChanged(PlayStatus.Playing);
    }

    private async Task OnStopTrack(object sender, StopTrackEventArgs e)
    {
        if (Track == null)
            return;

        Track = null;
        UpdatePlayState(false, false);
        StateHasChanged();
        await Player.Stop();
        await OnStatusChanged(PlayStatus.Stopped);
    }
}
