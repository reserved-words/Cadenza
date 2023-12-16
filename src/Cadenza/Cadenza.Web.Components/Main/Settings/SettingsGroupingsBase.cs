namespace Cadenza.Web.Components.Main.Settings;

public class SettingsGroupingsBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<GroupingsState> State { get; set; }

    protected IReadOnlyCollection<GroupingVM> Groupings => State.Value.Groupings;

    public List<EditableGrouping> EditableGroupings { get; set; }

    protected override void OnInitialized()
    {
        State.StateChanged += State_StateChanged;

        SetEditableGroupings();

        base.OnInitialized();
    }

    private void State_StateChanged(object sender, EventArgs e)
    {
        SetEditableGroupings();
    }

    protected void OnGroupingEdited(EditableGrouping grouping)
    {
        if (!string.IsNullOrWhiteSpace(grouping.Name))
        {
            EditableGroupings.Add(new EditableGrouping());
        }
    }

    protected void OnRemoveGrouping(EditableGrouping grouping)
    {
        EditableGroupings.Remove(grouping);
    }

    private void SetEditableGroupings()
    {
        EditableGroupings = State.Value.Groupings
            .Select(g => new EditableGrouping { Id = g.Id, Name = g.Name, IsUsed = g.IsUsed })
            .OrderByDescending(g => g.IsUsed)
            .ThenBy(g => g.Name)
            .ToList();

        EditableGroupings.Add(new EditableGrouping());
    }
}
