using Cadenza.Common.Domain.Model;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record SearchItemsState(bool IsLoading, List<PlayerItem> Items)
{
    private static SearchItemsState Init() => new SearchItemsState(true, new List<PlayerItem>());
}
