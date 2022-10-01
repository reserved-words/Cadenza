using Cadenza.Web.Player.Events;
using Cadenza.Web.Player.Interfaces;

namespace Cadenza.Web.Player;

public class PlayerBase : ComponentBase
{
    [Inject]
    public ICurrentTrackStore Store { get; set; }

    [Inject]
    internal IPlayer Player { get; set; }

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
        // Note - this may need to do something to account for situations where a new playlist is started while another track is playing
        // Currently the already playing track won't get scrobbled as it won't get formally stopped in the player - just replaced by the new track

        // Also need to look into weird flashes on first starting a playlist

        if (Track != null)
        {
            Model = await Store.GetCurrentTrack();
            await Player.Play(Track);
            UpdatePlayState(false, true);
            await OnTrackStatusChanged(new TrackStatusEventArgs { Track = Track, Status = PlayStatus.Playing });
        }
        else
        {
            Model = null;
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
