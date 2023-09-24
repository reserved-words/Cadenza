using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record LocalSourceConnectionState(TaskState State, string Message)
{
    private static LocalSourceConnectionState Init() => new LocalSourceConnectionState(TaskState.None, null);
}
