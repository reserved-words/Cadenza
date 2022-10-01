namespace Cadenza.Web.Player;

public class PlayerBase : ComponentBase
{
    [Inject]
    public ITrackRepository Repository { get; set; }

    [Inject]
    public IPlayer Player { get; set; }

    [Inject]
    internal ITrackFinishedConsumer TrackFinishedConsumer { get; set; }

    [Parameter]
    public PlayTrack Track { get; set; }

    [Parameter]
    public bool Loading { get; set; }

    [Parameter]
    public bool CanSkipNext { get; set; }

    [Parameter]
    public Func<Task> OnSkipNext { get; set; }

    [Parameter]
    public Func<Task> OnSkipPrevious { get; set; }

    [Parameter]
    public Func<TrackStatusEventArgs, Task> OnTrackStatusChanged { get; set; }

    public TrackFull Model { get; set; }

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
            Model = await Repository.GetTrack(Track.Id);
            CanPause = true;
            CanPlay = false;
            await OnTrackStatusChanged(new TrackStatusEventArgs { Track = Track, Status = PlayStatus.Playing });
        }
        else
        {
            Model = null;
        }

        StateHasChanged();
    }

    public bool CanPause { get; set; }

    public bool CanPlay { get; set; }

    public bool CanSkipPrevious => CanPlay || CanPause;

    public async Task Pause()
    {
        CanPlay = true;
        CanPause = false;
        StateHasChanged();

        var progress = await Player.Pause();
        await OnTrackStatusChanged(new TrackStatusEventArgs { Track = Track, Status = PlayStatus.Paused });
    }

    public async Task Resume()
    {
        CanPause = true;
        CanPlay = false;
        StateHasChanged();

        var progress = await Player.Resume();
        await OnTrackStatusChanged(new TrackStatusEventArgs { Track = Track, Status = PlayStatus.Playing });
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

    private async Task OnTrackFinished(object sender, TrackFinishedEventArgs e)
    {
        await StopPlaying();
        await OnTrackStatusChanged(new TrackStatusEventArgs { Track = Track, Status = PlayStatus.Stopped });
    }

    private async Task StopPlaying()
    {
        CanPause = false;
        CanPlay = false;
        StateHasChanged();

        await Player.Stop();
    }
}
