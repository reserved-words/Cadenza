namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record GroupingsState(bool IsLoading, IReadOnlyCollection<GroupingVM> Groupings)
{
    private static GroupingsState Init() => new GroupingsState(false, new ReadOnlyCollection<GroupingVM>(new List<GroupingVM>()));
}
