using Cadenza.Common.Domain.Model;

namespace Cadenza.State.Actions;

public record PlayStatusPausedAction();

public record PlayStatusResumedAction();

public record PlayStatusStoppedAction();

public record PlayStatusPlayingAction(PlayTrack Track);
