using Cadenza.Web.Common.Interfaces.View;

namespace Cadenza.Web.Player.Components;

public class CurrentTrackArtworkBase : ComponentBase
{
    [Inject]
    public IArtworkFetcher ArtworkFetcher { get; set; }

    [Inject]
    public IItemViewer ItemViewer { get; set; }

    [Parameter]
    public TrackFull Model { get; set; }

    public string AlbumDisplay { get; private set; }

    public string ArtworkUrl { get; private set; }

    protected override void OnParametersSet()
    {
        AlbumDisplay = Model == null
            ? null
            : $"{Model.Album.Title} ({Model.Album.ArtistName})";

        ArtworkUrl = ArtworkFetcher.GetArtworkUrl(Model?.Album);

        StateHasChanged();
    }

    protected async Task OnViewAlbum()
    {
        await ItemViewer.ViewAlbum(Model.Album.Id, Model.Album.Title);
    }
}
