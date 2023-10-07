namespace Cadenza.State.Actions;

public record TrackUpdateRequest(int TrackId, TrackUpdateVM Update);
public record TrackUpdatedAction(int TrackId, TrackUpdateVM Update);
public record TrackUpdateFailedAction(int TrackId);