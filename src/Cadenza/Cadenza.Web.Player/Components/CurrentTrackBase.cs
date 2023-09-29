using Cadenza.Common.Domain.Model.Library;
using Cadenza.State.Actions;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Web.Player.Components;

public class CurrentTrackBase : FluxorComponent
{
    [Parameter] public bool Empty { get; set; }
    [Parameter] public bool Loading { get; set; }
    [Parameter] public TrackFull Model { get; set; }

    protected override void OnInitialized()
    {
        SubscribeToAction<AlbumUpdatedAction>(OnAlbumUpdated);
        SubscribeToAction<ArtistUpdatedAction>(OnArtistUpdated);
        SubscribeToAction<TrackUpdatedAction>(OnTrackUpdated);
        base.OnInitialized();
    }

    private void OnAlbumUpdated(AlbumUpdatedAction action)
    {
        if (Model == null || action.Update.Id != Model.Album.Id)
            return;

        action.Update.ApplyUpdates(Model.Album);
        StateHasChanged();
    }

    private void OnArtistUpdated(ArtistUpdatedAction action)
    {
        if (Model == null || action.Update.Id != Model.Artist.Id)
            return;

        action.Update.ApplyUpdates(Model.Artist);
        StateHasChanged();
    }

    private void OnTrackUpdated(TrackUpdatedAction action)
    {
        if (Model == null || action.Update.Id != Model.Id)
            return;

        action.Update.ApplyUpdates(Model.Track);
        StateHasChanged();

    }
}
