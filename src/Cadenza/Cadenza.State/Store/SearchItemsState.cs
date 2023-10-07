namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record SearchItemsState(bool IsLoading, List<PlayerItemVM> Items)
{
    private static SearchItemsState Init() => new SearchItemsState(true, new List<PlayerItemVM>());
}
