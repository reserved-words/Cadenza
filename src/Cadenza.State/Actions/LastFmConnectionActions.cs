namespace Cadenza.State.Actions;

public record LastFmConnectRequest();
public record LastFmFetchTokenRequest();
public record LastFmFetchTokenResult(string Token);
public record LastFmFetchSessionKeyRequest(string Token);
public record LastFmFetchSessionKeyResult(string SessionKey, bool Reload);
public record LastFmConnectionFailedAction();
public record LastFmConnectedAction();
