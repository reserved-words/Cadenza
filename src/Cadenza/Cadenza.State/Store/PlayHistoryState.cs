using Cadenza.Common.Domain.Enums;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlayHistoryAlbumsState(bool IsLoading, IReadOnlyCollection<PlayedAlbumVM> Items, HistoryPeriod Period) 
{
    private static PlayHistoryAlbumsState Init() => new PlayHistoryAlbumsState(true, null, HistoryPeriod.Week);
}

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlayHistoryArtistsState(bool IsLoading, IReadOnlyCollection<PlayedArtistVM> Items, HistoryPeriod Period)
{
    private static PlayHistoryArtistsState Init() => new PlayHistoryArtistsState(true, null, HistoryPeriod.Week);
}

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlayHistoryTracksState(bool IsLoading, IReadOnlyCollection<PlayedTrackVM> Items, HistoryPeriod Period)
{
    private static PlayHistoryTracksState Init() => new PlayHistoryTracksState(true, null, HistoryPeriod.Week);
}
