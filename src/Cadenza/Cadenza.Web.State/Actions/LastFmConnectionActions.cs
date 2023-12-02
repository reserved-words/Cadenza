namespace Cadenza.Web.State.Actions;

public record LastFmConnectRequest();
public record LastFmFetchTokenRequest();
public record LastFmFetchTokenResult(string Token);
public record LastFmCreateSessionRequest(string Token);
public record LastFmConnectionFailedAction();
public record LastFmConnectedAction();
