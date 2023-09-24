using Cadenza.State.Interfaces;
using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record LastFmConnectionState(string Title, TaskState State, string Message, string SessionKey) : IConnectionState
{
    private static LastFmConnectionState Init() => new LastFmConnectionState(null, TaskState.None, null, null);
}
