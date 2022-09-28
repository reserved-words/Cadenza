using Cadenza.Domain.Extensions;
using Cadenza.Domain.Model.Track;
using Cadenza.Web.Common.Enums;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Common.Interop;
using Cadenza.Web.Core.Events;
using Cadenza.Web.Core.Interfaces;
using Cadenza.Web.Core.Player;

namespace Cadenza.UI.Components.Sidebar;

public class CurrentTrackBase : ComponentBase
{
    [Inject]
    public ITrackProgressedConsumer TrackProgressConsumer { get; set; }

    [Inject]
    public IAppConsumer App { get; set; }

    [Inject]
    public IStoreGetter Store { get; set; }

    [Inject]
    public IArtworkFetcher ArtworkFetcher { get; set; }

    public bool Loading { get; set; } = false;

    public bool Empty => _model == null && !Loading;

    public double Progress { get; set; }

    public string AlbumId => _model?.Album.Id;
    public string AlbumArtistId => _model?.Album.ArtistId;
    public string TrackId => _model?.Track.Id;
    public string TrackArtistId => _model?.Artist.Id;

    public string AlbumArtist => _model?.Album.ArtistName ?? "Album Artist";

    public string AlbumTitle => _model?.Album.Title ?? "Album Title";

    public string Artist => _model?.Artist.Name ?? "Artist Name";

    public string ReleaseType => _model?.Album.ReleaseType.GetDisplayName() ?? "Release Type";

    public string SourceIcon => _model?.Track.Source.GetIcon();

    public string Title => _model?.Track.Title ?? "Track Title";

    public string Year => _model?.Album.Year ?? "Year";

    public string ArtworkUrl { get; private set; }

    private TrackFull _model;

    protected override void OnInitialized()
    {
        App.PlaylistLoading += OnPlaylistLoading;
        App.PlaylistFinished += OnPlaylistFinished;

        App.TrackStarted += OnTrackStarted;
        App.TrackFinished += OnTrackFinished;

        TrackProgressConsumer.TrackProgressed += OnTrackProgressed;
    }

    private async Task OnPlaylistFinished(object sender, PlaylistEventArgs e)
    {
        Loading = false;
        await UpdateModel(null);
        Progress = 0;
        StateHasChanged();
    }

    private async Task OnPlaylistLoading(object sender, PlaylistEventArgs e)
    {
        await UpdateModel(null);
        Loading = true;
        Progress = 0;
        StateHasChanged();
    }

    private async Task OnTrackFinished(object sender, TrackEventArgs e)
    {
        if (e.IsLastTrack)
        {
            await UpdateModel(null);
            Progress = 0;
            StateHasChanged();
        }
    }

    private async Task UpdateModel(TrackFull track)
    {
        _model = track;
        ArtworkUrl = await ArtworkFetcher.GetArtworkUrl(_model?.Album, _model?.Track.Id);
    }

    private async Task OnTrackStarted(object sender, TrackEventArgs e)
    {
        Loading = false;
        Progress = 0;
        await UpdateModel((await Store.GetValue<TrackFull>(StoreKey.CurrentTrack)).Value);
        StateHasChanged();
    }

    private void OnTrackProgressed(object sender, TrackProgressedEventArgs e)
    {
        Progress = e.ProgressPercentage;
        StateHasChanged();
    }
}
