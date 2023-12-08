using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record EditTrackState(bool IsLoading, TrackDetailsVM Track)
{
    private static EditTrackState Init() => new EditTrackState(true, null);
}