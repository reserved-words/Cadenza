using Cadenza.Web.Common.Enums;

namespace Cadenza.State.Actions;

public record ConnectorStatusUpdateRequest(Connector Connector, ConnectorStatus Status, Exception Error);

public record ConnectorStatusUpdatedAction(Connector Connector, ConnectorStatus Status);
