namespace Cadenza.Core
{
    public interface IConnectorController
    {
        Task SetStatus(Connector connector, ConnectorStatus status, string error = null);
    }
}
