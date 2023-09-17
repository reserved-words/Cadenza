using Cadenza.Common.Domain.Model.Track;
namespace Cadenza.State.Actions;

public record PlayStatusPausedAction(int SecondsPlayed);

public record PlayStatusResumedAction(int SecondsPlayed);

public record PlayStatusStoppedAction(int SecondsPlayed);

public record PlayStatusPlayingAction();
