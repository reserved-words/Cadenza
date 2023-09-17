using Cadenza.Common.Domain.Model;

namespace Cadenza.State.Actions;

public record PlayerPlayRequest(PlayTrack Track);
public record PlayerPauseRequest();
public record PlayerResumeRequest();
public record PlayerStopRequest();
