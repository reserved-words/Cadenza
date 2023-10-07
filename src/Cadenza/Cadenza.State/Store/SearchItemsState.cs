namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record SearchItemsState(bool IsLoading, IReadOnlyCollection<PlayerItemVM> Items)
{
    private static SearchItemsState Init() => new SearchItemsState(true, new ReadOnlyCollection<PlayerItemVM>(new List<PlayerItemVM>()));
}
