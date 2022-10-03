namespace Cadenza.Web.Common.Interfaces.Connections;

public interface IConnectionCoordinator
{
    Task SetStatus(Connector connector, ConnectorStatus status, string error = null);
}
