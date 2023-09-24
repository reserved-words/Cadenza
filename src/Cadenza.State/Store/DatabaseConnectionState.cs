using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record DatabaseConnectionState(TaskState State, string Message)
{
    private static DatabaseConnectionState Init() => new DatabaseConnectionState(TaskState.None, null);
}