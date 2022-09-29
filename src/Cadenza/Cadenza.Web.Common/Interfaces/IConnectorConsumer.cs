
namespace Cadenza.Web.Common.Interfaces
{
    public interface IConnectorConsumer
    {
        ConnectorStatus GetStatus(Connector connector);
        event ConnectorEventHandler ConnectorStatusChanged;
    }
}
