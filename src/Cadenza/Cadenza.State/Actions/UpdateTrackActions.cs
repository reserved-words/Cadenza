namespace Cadenza.State.Actions;

public record TrackUpdateRequest(TrackDetailsVM OriginalTrack, EditableTrack Update);
public record TrackUpdatedAction(TrackDetailsVM UpdatedTrack);
public record TrackUpdateFailedAction(int TrackId);