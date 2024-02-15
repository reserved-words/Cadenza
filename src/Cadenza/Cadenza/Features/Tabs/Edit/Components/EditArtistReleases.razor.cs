namespace Cadenza.Features.Tabs.Edit.Components;

public class EditArtistReleasesBase : ComponentBase
{
    [Parameter] public List<EditableArtistRelease> Model { get; set; }

    protected IReadOnlyCollection<ReleaseType> ReleaseTypes => Enum.GetValues<ReleaseType>();
}
