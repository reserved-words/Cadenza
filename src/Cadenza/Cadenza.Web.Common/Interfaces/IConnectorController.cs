namespace Cadenza.Web.Common.Interfaces
{
    public interface IConnectorController
    {
        Task SetStatus(Connector connector, ConnectorStatus status, string error = null);
    }
}
