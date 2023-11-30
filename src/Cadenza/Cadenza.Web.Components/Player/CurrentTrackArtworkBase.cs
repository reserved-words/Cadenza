using Cadenza.Web.Common.Interfaces;

namespace Cadenza.Web.Components.Player;

public class CurrentTrackArtworkBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    [Parameter] public TrackFullVM Model { get; set; }

    public string AlbumDisplay { get; private set; }
    public string ArtworkUrl { get; private set; }

    protected override void OnParametersSet()
    {
        AlbumDisplay = Model == null
            ? null
            : $"{Model.Album.Title} ({Model.Album.ArtistName})";

        ArtworkUrl = Model == null
            ? "images/artwork-placeholder.png" // TODO - put this somewhere else
            : Model.Album.ImageUrl;
    }

    protected void OnViewAlbum()
    {
        Dispatcher.Dispatch(new ViewItemRequest(PlayerItemType.Album, Model.Album.Id.ToString(), Model.Album.Title));
    }
}
