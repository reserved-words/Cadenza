namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record LastFmConnectionState(string Title, ConnectionState State, string Message)
{
    public static LastFmConnectionState Init() => new LastFmConnectionState(null, ConnectionState.None, null);
}
