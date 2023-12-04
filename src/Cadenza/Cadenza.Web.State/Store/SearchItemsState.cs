using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record SearchItemsState(bool IsLoading, IReadOnlyCollection<SearchItemVM> Items)
{
    private static SearchItemsState Init() => new SearchItemsState(true, new ReadOnlyCollection<SearchItemVM>(new List<SearchItemVM>()));
}
