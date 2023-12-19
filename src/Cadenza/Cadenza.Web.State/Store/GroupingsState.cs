namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record GroupingsState(bool IsLoading, IReadOnlyCollection<string> Groupings)
{
    private static GroupingsState Init() => new GroupingsState(false, new ReadOnlyCollection<string>(new List<string>()));
}
