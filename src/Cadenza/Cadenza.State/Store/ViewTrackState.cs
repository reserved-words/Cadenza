using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewTrackState(bool IsLoading, TrackFull Track) 
{
    private static ViewTrackState Init() => new ViewTrackState(true, null);
}