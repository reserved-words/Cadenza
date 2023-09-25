using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record LocalSourceConnectionState(string Title, TaskState State, string Message) : IConnectionState
{
    public static LocalSourceConnectionState Init() => new LocalSourceConnectionState(null, TaskState.None, null);
}
