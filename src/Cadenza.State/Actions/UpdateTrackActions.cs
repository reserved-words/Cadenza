using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.State.Actions;

public record TrackUpdateRequest(int TrackId, TrackUpdate Update);
public record TrackUpdatedAction(int TrackId, TrackUpdate Update);
public record TrackUpdateFailedAction(int TrackId);