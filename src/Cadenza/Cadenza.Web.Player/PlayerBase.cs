using Cadenza.Common.Domain.Extensions;
using Cadenza.Common.Domain.Model;
using Cadenza.Common.Domain.Model.Track;
using Cadenza.Common.Interfaces.Repositories;
using Cadenza.Web.Common.Events;
using Cadenza.Web.Common.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Cadenza.Web.Player;

public class PlayerBase : ComponentBase
{
    [Inject]
    public IArtworkFetcher ArtworkFetcher { get; set; }

    [Inject]
    public ITrackRepository Repository { get; set; }

    [Inject]
    public IPlayer Player { get; set; }

    [Inject]
    internal ITrackFinishedConsumer TrackFinishedConsumer { get; set; }

    [Inject]
    internal ITrackProgressedConsumer TrackProgressConsumer { get; set; }

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

    private TrackFull Model { get; set; }

    public bool Empty => Model == null && !Loading;

    public double Progress { get; set; }

    public string AlbumId => Model?.Album.Id;
    public string AlbumArtistId => Model?.Album.ArtistId;
    public string TrackId => Model?.Track.Id;
    public string TrackArtistId => Model?.Artist.Id;

    public string AlbumArtist => Model?.Album.ArtistName ?? "Album Artist";

    public string AlbumTitle => Model?.Album.Title ?? "Album Title";

    public string Artist => Model?.Artist.Name ?? "Artist Name";

    public string ReleaseType => Model?.Album.ReleaseType.GetDisplayName() ?? "Release Type";

    public string Title => Model?.Track.Title ?? "Track Title";

    public string Year => Model?.Album.Year ?? "Year";

    public string ArtworkUrl { get; private set; }

    protected override void OnInitialized()
    {
        TrackFinishedConsumer.TrackFinished += OnTrackFinished;
        TrackProgressConsumer.TrackProgressed += OnTrackProgressed;
    }

    protected override async Task OnParametersSetAsync()
    {
        if (Track != null)
        {
            await Player.Play(Track);
            Model = await Repository.GetTrack(Track.Id);
            CanPause = true;
            CanPlay = false;
            ArtworkUrl = await ArtworkFetcher.GetArtworkUrl(Model.Album, Model.Track.Id);
            await OnTrackStatusChanged(new TrackStatusEventArgs { Track = Track, Status = PlayStatus.Playing });
            StateHasChanged();
        }
    }

    public bool CanPause { get; set; }

    public bool CanPlay { get; set; }

    public bool CanSkipPrevious => CanPlay || CanPause;

    public async Task Pause()
    {
        var progress = await Player.Pause();
        CanPlay = true;
        CanPause = false;
        await OnTrackStatusChanged(new TrackStatusEventArgs { Track = Track, Status = PlayStatus.Paused });
    }

    public async Task Resume()
    {
        var progress = await Player.Resume();
        CanPause = true;
        CanPlay = false;
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

    private void OnTrackProgressed(object sender, TrackProgressedEventArgs e)
    {
        Progress = e.ProgressPercentage;
        StateHasChanged();
    }

    private async Task StopPlaying()
    {
        await Player.Stop();
        Model = null;
        CanPause = false;
        CanPlay = true;
        ArtworkUrl = await ArtworkFetcher.GetArtworkUrl(null);
        StateHasChanged();
    }
}
