using Cadenza.Web.Common.Enums;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlayStatusState(PlayStatus Status, int TrackId) 
{
    private static PlayStatusState Init() => new PlayStatusState(PlayStatus.Stopped, 0);
}