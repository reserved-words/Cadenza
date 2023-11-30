using System.Collections.ObjectModel;

namespace Cadenza.Web.Components.Forms.Album;

public class EditAlbumBase : FormBase<AlbumDetailsVM>
{
    [Inject] public IDispatcher Dispatcher { get; set; }

    protected EditableAlbum EditableItem { get; set; }

    protected override void OnParametersSet()
    {
        EditableItem = new EditableAlbum
        {
            Id = Model.Id,
            ArtistId = Model.ArtistId,
            ArtistName = Model.ArtistName,
            Title = Model.Title,
            ReleaseType = Model.ReleaseType,
            Year = Model.Year,
            ArtworkBase64 = Model.ArtworkBase64,
            Tags = Model.Tags.ToList(),
            ImageUrl = Model.ImageUrl
        };
    }

    protected override void OnInitialized()
    {
        SubscribeToAction<AlbumUpdatedAction>(OnAlbumUpdated);
        base.OnInitialized();
    }

    protected void OnSubmit()
    {
        var updatedAlbum = Model with
        {
            Title = EditableItem.Title,
            ReleaseType = EditableItem.ReleaseType,
            Year = EditableItem.Year,
            ArtworkBase64 = EditableItem.ArtworkBase64,
            Tags = new ReadOnlyCollection<string>(EditableItem.Tags.ToList())
        };

        Dispatcher.Dispatch(new AlbumUpdateRequest(Model, updatedAlbum));
    }

    private void OnAlbumUpdated(AlbumUpdatedAction action)
    {
        Submit();
    }

    protected void OnCancel()
    {
        Cancel();
    }
}
