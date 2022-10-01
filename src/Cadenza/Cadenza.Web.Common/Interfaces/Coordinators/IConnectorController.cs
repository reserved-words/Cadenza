namespace Cadenza.Web.Common.Interfaces.Coordinators;

public interface IConnectorController
{
    Task SetStatus(Connector connector, ConnectorStatus status, string error = null);
}
