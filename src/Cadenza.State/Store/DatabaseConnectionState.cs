using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record DatabaseConnectionState(string Title, TaskState State, string Message) : IConnectionState
{
    public static DatabaseConnectionState Init() => new DatabaseConnectionState(null, TaskState.None, null);
}