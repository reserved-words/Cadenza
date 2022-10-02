namespace Cadenza.Web.Common.Interfaces.Connections;

public interface IConnectionController
{
    Task SetStatus(Connector connector, ConnectorStatus status, string error = null);
}
