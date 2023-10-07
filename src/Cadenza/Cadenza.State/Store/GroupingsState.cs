namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record GroupingsState(bool IsLoading, List<GroupingVM> Groupings)
{
    private static GroupingsState Init() => new GroupingsState(false, new List<GroupingVM>());
}
