using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record LastFmConnectionState(string Title, TaskState State, string Message, string SessionKey) : IConnectionState
{
    public static LastFmConnectionState Init() => new LastFmConnectionState(null, TaskState.None, null, null);
}
