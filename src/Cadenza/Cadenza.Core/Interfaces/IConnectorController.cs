using Cadenza.Core.Common;

namespace Cadenza.Core.Interfaces
{
    public interface IConnectorController
    {
        Task SetStatus(Connector connector, ConnectorStatus status, string error = null);
    }
}
