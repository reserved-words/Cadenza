namespace Cadenza.Web.Player;

public class PlayerBase : ComponentBase
{
    [Inject]
    public ICurrentTrackStore Store { get; set; }

    [Inject]
    public IPlayer Player { get; set; }

    [Inject]
    internal ITrackFinishedConsumer TrackFinishedConsumer { get; set; }

    [Parameter]
    public PlayTrack Track { get; set; }

    [Parameter]
    public bool Loading { get; set; }

    [Parameter]
    public bool IsLastTrack { get; set; }

    [Parameter]
    public Func<Task> OnSkipNext { get; set; }

    [Parameter]
    public Func<Task> OnSkipPrevious { get; set; }

    [Parameter]
    public Func<TrackStatusEventArgs, Task> OnTrackStatusChanged { get; set; }

    public TrackFull Model { get; set; }

    public bool CanPause { get; set; }

    public bool CanPlay { get; set; }

    public bool CanSkipNext { get; set; }

    public bool Empty => Model == null && !Loading;

    protected override void OnInitialized()
    {
        TrackFinishedConsumer.TrackFinished += OnTrackFinished;
    }

    protected override async Task OnParametersSetAsync()
    {
        if (Track != null)
        {
            await Player.Play(Track);
            Model = await Store.GetCurrentTrack();
            UpdatePlayState(false, true);
            await OnTrackStatusChanged(new TrackStatusEventArgs { Track = Track, Status = PlayStatus.Playing });
        }
        else
        {
            Model = null;
         //   StateHasChanged();
        }
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

    public async Task SkipNext()
    {
        await StopPlaying();
        await OnSkipNext();
    }

    public async Task SkipPrevious()
    {
        await StopPlaying();
        await OnSkipPrevious();
    }

    private async Task OnStatusChanged(PlayStatus status)
    {
        await OnTrackStatusChanged(new TrackStatusEventArgs { Track = Track, Status = status });
    }

    private async Task OnTrackFinished(object sender, TrackFinishedEventArgs e)
    {
        await StopPlaying();
        await OnStatusChanged(PlayStatus.Stopped);
    }

    private async Task StopPlaying()
    {
        UpdatePlayState(false, false);
        await Player.Stop();
    }

    private void UpdatePlayState(bool canPlay, bool canPause)
    {
        CanPlay = canPlay;
        CanPause = canPause;
        CanSkipNext = (canPlay || canPause) && !IsLastTrack; 
        StateHasChanged();
    }
}
