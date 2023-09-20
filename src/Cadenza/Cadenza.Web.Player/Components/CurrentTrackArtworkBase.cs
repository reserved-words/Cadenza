using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.Web.Player.Components;

public class CurrentTrackArtworkBase : ComponentBase
{
    [Inject]
    public IArtworkFetcher ArtworkFetcher { get; set; }

    [Inject]
    public IDispatcher Dispatcher { get; set; }

    [Parameter]
    public TrackFull Model { get; set; }

    public string AlbumDisplay { get; private set; }

    public string ArtworkUrl { get; private set; }

    protected override void OnParametersSet()
    {
        AlbumDisplay = Model == null
            ? null
            : $"{Model.Album.Title} ({Model.Album.ArtistName})";

        ArtworkUrl = ArtworkFetcher.GetAlbumArtworkSrc(Model?.Album);

        StateHasChanged();
    }

    protected void OnViewAlbum()
    {
        Dispatcher.Dispatch(new ViewItemRequest(PlayerItemType.Album, Model.Album.Id.ToString(), Model.Album.Title));
    }
}
