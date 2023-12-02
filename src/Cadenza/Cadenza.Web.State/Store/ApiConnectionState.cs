namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ApiConnectionState(string Title, ConnectionState State, string Message)
{
    public static ApiConnectionState Init() => new ApiConnectionState(null, ConnectionState.None, null);
}