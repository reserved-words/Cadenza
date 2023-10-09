namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record LastFmConnectionState(string Title, ConnectionState State, string Message, string SessionKey)
{
    public static LastFmConnectionState Init() => new LastFmConnectionState(null, ConnectionState.None, null, null);
}
