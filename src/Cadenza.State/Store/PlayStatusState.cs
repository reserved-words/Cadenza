using Cadenza.Common.Domain.Model;
using Cadenza.Web.Common.Enums;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlayStatusState(PlayStatus Status, PlayTrack Track) 
{
    private static PlayStatusState Init() => new PlayStatusState(PlayStatus.Stopped, null);
}