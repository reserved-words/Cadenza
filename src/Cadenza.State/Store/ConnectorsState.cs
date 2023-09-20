using Cadenza.Web.Common.Enums;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ConnectorState(Dictionary<Connector, ConnectorStatus> Connectors)
{
    private static ConnectorState Init() => new ConnectorState(new Dictionary<Connector, ConnectorStatus>());
}
