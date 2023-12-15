using Cadenza.Common.Enums;
using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record HistoryTopPlayedAlbumsState(bool IsLoading, IReadOnlyCollection<TopAlbumVM> Items, HistoryPeriod Period)
{
    private static HistoryTopPlayedAlbumsState Init() => new HistoryTopPlayedAlbumsState(true, null, HistoryPeriod.Week);
}
