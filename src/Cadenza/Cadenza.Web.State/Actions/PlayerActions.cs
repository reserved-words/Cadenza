using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record PlayerPlayRequest(TrackFullVM Track);
public record PlayerPauseRequest(TrackFullVM Track);
public record PlayerResumeRequest(TrackFullVM Track);
public record PlayerStopRequest(TrackFullVM Track);
