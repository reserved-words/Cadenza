namespace Cadenza.State.Actions;

public record TrackRemovalRequest(int TrackId);
public record TrackRemovedAction(int TrackId);
public record TrackRemovalFailedAction(int TrackId);