namespace Cadenza.State.Actions;

public record LastFmConnectRequest();
public record LastFmFetchTokenRequest();
public record LastFmFetchTokenResult(string Token);
public record LastFmFetchSessionKeyRequest();
public record LastFmFetchSessionKeyResult(string SessionKey);
public record LastFmConnectedAction();
