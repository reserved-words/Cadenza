namespace Cadenza.Web.Components.Main.Edit;

public class EditArtistDetailsBase : ComponentBase
{
    [Inject] public IState<GroupingsState> GroupingsState { get; set; }

    [Parameter] public EditableArtist Model { get; set; }

    protected IReadOnlyCollection<string> Groupings => GroupingsState.Value.Groupings;
}
