namespace Cadenza.Web.Components.Main.Edit;

public class EditArtistReleasesBase : ComponentBase
{
    [Parameter] public List<EditableArtistRelease> Model { get; set; }

    protected IReadOnlyCollection<ReleaseType> ReleaseTypes => Enum.GetValues<ReleaseType>();
}
