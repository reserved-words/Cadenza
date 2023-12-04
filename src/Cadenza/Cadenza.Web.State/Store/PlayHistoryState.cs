using Cadenza.Common.Enums;
using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlayHistoryAlbumsState(bool IsLoading, IReadOnlyCollection<TopAlbumVM> Items, HistoryPeriod Period)
{
    private static PlayHistoryAlbumsState Init() => new PlayHistoryAlbumsState(true, null, HistoryPeriod.Week);
}

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlayHistoryArtistsState(bool IsLoading, IReadOnlyCollection<TopArtistVM> Items, HistoryPeriod Period)
{
    private static PlayHistoryArtistsState Init() => new PlayHistoryArtistsState(true, null, HistoryPeriod.Week);
}

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlayHistoryTracksState(bool IsLoading, IReadOnlyCollection<TopTrackVM> Items, HistoryPeriod Period)
{
    private static PlayHistoryTracksState Init() => new PlayHistoryTracksState(true, null, HistoryPeriod.Week);
}
