using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Actions;

public record ApplicationStartRequest();
public record ApplicationStartedAction();
public record ApplicationStartupProgressAction(Connector Connector, ConnectionState State, string Message);