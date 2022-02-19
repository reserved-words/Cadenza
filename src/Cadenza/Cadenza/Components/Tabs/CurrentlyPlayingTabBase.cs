using Cadenza.Library;

namespace Cadenza;

public class CurrentlyPlayingTabBase : ComponentBase
{
    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IMergedTrackRepository TrackRepository { get; set; }

    public TrackFull Model { get; set; }

    public bool NotCurrentlyPlaying => Model == null;

    protected override void OnInitialized()
    {
        App.TrackStarted += OnTrackStarted;
        App.TrackFinished += OnTrackFinished;
    }

    private async Task OnTrackStarted(object sender, TrackEventArgs e)
    {
        await SetTrack(e.CurrentTrack.Source, e.CurrentTrack.Id);
    }

    private async Task OnTrackFinished(object sender, TrackEventArgs e)
    {
        await SetTrack(null, null);
    }

    private async Task SetTrack(LibrarySource? source, string trackId)
    {
        Model = source.HasValue
            ? await TrackRepository.GetTrack(source.Value, trackId)
            : null;

        StateHasChanged();
    }
}