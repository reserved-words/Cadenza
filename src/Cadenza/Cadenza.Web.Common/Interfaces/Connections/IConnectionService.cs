namespace Cadenza.Web.Common.Interfaces.Connections;

public interface IConnectionService
{
    ConnectorStatus GetStatus(Connector connector);
}