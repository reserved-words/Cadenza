using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Components.ViewBases;

public class AlbumViewBase : FluxorComponent
{
    [Parameter] public AlbumInfo Model { get; set; } = new();

    protected override void OnInitialized()
    {
        SubscribeToAction<AlbumUpdatedAction>(OnAlbumUpdated);
        base.OnInitialized();
    }

    private void OnAlbumUpdated(AlbumUpdatedAction action)
    {
        if (Model != null && Model.Id == action.Update.Id)
        {
            action.Update.ApplyUpdates(Model);
            StateHasChanged();
        }
    }
}
