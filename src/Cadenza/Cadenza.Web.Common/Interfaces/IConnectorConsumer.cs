using Cadenza.Web.Common.Enums;
using Cadenza.Web.Common.Events;

namespace Cadenza.Web.Common.Interfaces
{
    public interface IConnectorConsumer
    {
        ConnectorStatus GetStatus(Connector connector);
        event ConnectorEventHandler ConnectorStatusChanged;
    }
}
