using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.State.Actions;

public record PlayerPlayRequest(Track Track);
public record PlayerPauseRequest();
public record PlayerResumeRequest();
public record PlayerStopRequest();
