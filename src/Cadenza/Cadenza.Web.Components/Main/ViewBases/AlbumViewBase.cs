using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Components.Main.ViewBases;

public class AlbumViewBase : FluxorComponent
{
    [Parameter] public AlbumDetailsVM Model { get; set; } = new();

    protected override void OnInitialized()
    {
        SubscribeToAction<AlbumUpdatedAction>(OnAlbumUpdated);
        base.OnInitialized();
    }

    private void OnAlbumUpdated(AlbumUpdatedAction action)
    {
        if (Model != null && Model.Id == action.UpdatedAlbum.Id)
        {
            Model = action.UpdatedAlbum;
            StateHasChanged();
        }
    }
}
