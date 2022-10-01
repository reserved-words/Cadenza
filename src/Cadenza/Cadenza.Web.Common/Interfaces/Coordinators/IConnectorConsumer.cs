namespace Cadenza.Web.Common.Interfaces.Coordinators;

public interface IConnectorConsumer
{
    ConnectorStatus GetStatus(Connector connector);
    event ConnectorEventHandler ConnectorStatusChanged;
}
