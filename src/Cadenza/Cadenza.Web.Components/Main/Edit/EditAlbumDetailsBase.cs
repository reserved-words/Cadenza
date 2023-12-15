namespace Cadenza.Web.Components.Main.Edit;

public class EditAlbumDetailsBase : FluxorComponent
{
    [Parameter] public EditableAlbum Model { get; set; }

    protected IReadOnlyCollection<ReleaseType> ReleaseTypes => Enum.GetValues<ReleaseType>();

    protected override void OnInitialized()
    {
        SubscribeToAction<AlbumUpdateDiscCountRequest>(OnAlbumUpdateDiscCount);
        base.OnInitialized();
    }

    private void OnAlbumUpdateDiscCount(AlbumUpdateDiscCountRequest request)
    {
        Model.DiscCount = request.DiscCount;
    }
}
