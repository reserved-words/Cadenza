using Cadenza.State.Interfaces;
using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record DatabaseConnectionState(string Title, TaskState State, string Message) : IConnectionState
{
    private static DatabaseConnectionState Init() => new DatabaseConnectionState(null, TaskState.None, null);
}