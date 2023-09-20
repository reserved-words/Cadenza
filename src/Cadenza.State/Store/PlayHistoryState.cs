using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model.History;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlayHistoryAlbumsState(bool IsLoading, List<PlayedAlbum> Items, HistoryPeriod Period) 
{
    private static PlayHistoryAlbumsState Init() => new PlayHistoryAlbumsState(true, null, HistoryPeriod.Week);
}

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlayHistoryArtistsState(bool IsLoading, List<PlayedArtist> Items, HistoryPeriod Period)
{
    private static PlayHistoryArtistsState Init() => new PlayHistoryArtistsState(true, null, HistoryPeriod.Week);
}

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlayHistoryTracksState(bool IsLoading, List<PlayedTrack> Items, HistoryPeriod Period)
{
    private static PlayHistoryTracksState Init() => new PlayHistoryTracksState(true, null, HistoryPeriod.Week);
}
