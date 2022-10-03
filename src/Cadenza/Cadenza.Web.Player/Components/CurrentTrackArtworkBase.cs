namespace Cadenza.Web.Player.Components;

public class CurrentTrackArtworkBase : ComponentBase
{
    [Inject]
    public IArtworkFetcher ArtworkFetcher { get; set; }

    [Parameter]
    public TrackFull Model { get; set; }

    public string ArtworkUrl { get; private set; }

    protected override async Task OnParametersSetAsync()
    {
        ArtworkUrl = Model == null
            ? await ArtworkFetcher.GetArtworkUrl(null)
            : await ArtworkFetcher.GetArtworkUrl(Model.Album, Model.Track.Id);

        StateHasChanged();
    }
}
