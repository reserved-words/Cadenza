using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record LastFmConnectionState(TaskState State, string Message, string SessionKey)
{
    private static LastFmConnectionState Init() => new LastFmConnectionState(TaskState.None, null, null);
}
