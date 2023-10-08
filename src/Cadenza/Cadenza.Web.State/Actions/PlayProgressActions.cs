namespace Cadenza.Web.State.Actions;

public record PlayProgressIncrementAction();
public record PlayProgressResetAction(int TotalSeconds);
public record PlayProgressUpdateAction(double PercentagePlayed);