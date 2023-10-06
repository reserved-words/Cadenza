namespace Cadenza.State.Actions;

public record ApplicationStartRequest();
public record ApplicationStartedAction();
public record ApplicationStartupProgressAction(ConnectionType ConnectionType, ConnectionState State, string Message);