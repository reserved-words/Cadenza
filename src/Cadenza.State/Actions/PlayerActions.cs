using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.State.Actions;

public record PlayerPlayRequest(TrackFull Track);
public record PlayerPauseRequest();
public record PlayerResumeRequest();
public record PlayerStopRequest();
