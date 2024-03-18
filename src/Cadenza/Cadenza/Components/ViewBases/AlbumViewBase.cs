namespace Cadenza.Components.ViewBases;

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
            Model = Model with
            {
                Title = action.UpdatedAlbum.Title,
                ReleaseType = action.UpdatedAlbum.ReleaseType,
                Year = action.UpdatedAlbum.Year,
                DiscCount = action.UpdatedAlbum.DiscCount,
                ArtworkBase64 = action.UpdatedAlbum.ArtworkBase64,
                Tags = action.UpdatedAlbum.Tags
            };
            StateHasChanged();
        }
    }
}
