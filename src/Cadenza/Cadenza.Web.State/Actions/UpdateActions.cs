namespace Cadenza.Web.State.Actions;

public record UpdateSucceededAction(UpdateType Type, int Id);
public record UpdateFailedAction(UpdateType Type, int Id, string Error, string StackTrace);