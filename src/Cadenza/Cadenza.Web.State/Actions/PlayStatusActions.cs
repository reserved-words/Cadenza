using Cadenza.Web.Model;

namespace Cadenza.Web.State.Actions;

public record PlayStatusPausedAction(TrackFullVM Track, int SecondsPlayed);

public record PlayStatusResumedAction(TrackFullVM Track, int SecondsPlayed);

public record PlayStatusStoppedAction(TrackFullVM Track, int SecondsPlayed);

public record PlayStatusPlayingAction(TrackFullVM Track);
