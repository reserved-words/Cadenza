using Cadenza.Common.Enums;
using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record HistoryTopPlayedArtistsState(bool IsLoading, IReadOnlyCollection<TopArtistVM> Items, HistoryPeriod Period)
{
    private static HistoryTopPlayedArtistsState Init() => new HistoryTopPlayedArtistsState(true, null, HistoryPeriod.Week);
}
