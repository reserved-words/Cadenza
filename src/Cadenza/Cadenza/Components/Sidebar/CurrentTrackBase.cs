using Cadenza.Utilities;

namespace Cadenza;

public class CurrentTrackBase : ComponentBase
{
    [Inject]
    public ITrackProgressedConsumer TrackProgressConsumer { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IStoreGetter Store { get; set; }

    public bool Loading { get; set; } = false;

    public bool Empty => _model == null && !Loading;

    public double Progress { get; set; }

    public string AlbumArtist => _model?.AlbumArtist ?? "Album Artist";

    public string AlbumTitle => _model?.AlbumTitle ?? "Album Title";

    public string Artist => _model?.Artist ?? "Artist Name";

    public string ArtworkUrl => _model?.ArtworkUrl ?? "images/artwork-placeholder.png";

    public string ReleaseType => _model?.ReleaseType.GetDisplayName() ?? "Release Type";

    public string SourceIcon => _model?.Source.GetIcon();

    public string Title => _model?.Title ?? "Track Title";

    public string Year => _model?.Year ?? "Year";

    private TrackSummary _model;

    protected override void OnInitialized()
    {
        App.TrackStarted += OnTrackStarted;
        TrackProgressConsumer.TrackProgressed += OnTrackProgressed;
    }

    private async Task OnTrackStarted(object sender, TrackEventArgs e)
    {
        _model = await Store.GetValue<TrackSummary>(StoreKey.CurrentTrack);
        StateHasChanged();
    }

    private void OnTrackProgressed(object sender, TrackProgressedEventArgs e)
    {
        Progress = e.ProgressPercentage;
        StateHasChanged();
    }
}
