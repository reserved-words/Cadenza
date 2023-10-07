namespace Cadenza.State.Actions;

public record TrackUpdateRequest(int TrackId, TrackDetailsVM OriginalTrack, EditableTrack Update);
public record TrackUpdatedAction(int TrackId, TrackDetailsVM UpdatedTrack);
public record TrackUpdateFailedAction(int TrackId);