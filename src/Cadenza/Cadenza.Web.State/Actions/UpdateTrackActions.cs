using Cadenza.Web.Model;

namespace Cadenza.Web.State.Actions;

public record TrackUpdateRequest(TrackDetailsVM OriginalTrack, TrackDetailsVM UpdatedTrack);
public record TrackUpdatedAction(TrackDetailsVM UpdatedTrack);
public record TrackUpdateFailedAction(int TrackId);