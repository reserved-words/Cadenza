namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record DatabaseConnectionState(string Title, ConnectionState State, string Message)
{
    public static DatabaseConnectionState Init() => new DatabaseConnectionState(null, ConnectionState.None, null);
}