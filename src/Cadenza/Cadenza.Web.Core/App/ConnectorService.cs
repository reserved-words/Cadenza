using Cadenza.Web.Common.Enums;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Core.Interfaces;

namespace Cadenza.Web.Core.App
{
    public class ConnectorService : IConnectorConsumer, IConnectorController
    {
        private readonly Dictionary<Connector, ConnectorStatus> _statuses;

        public ConnectorService()
        {
            _statuses = Enum.GetValues<Connector>()
                .ToDictionary(c => c, c => ConnectorStatus.Loading);
        }

        public event ConnectorEventHandler ConnectorStatusChanged;

        public ConnectorStatus GetStatus(Connector connector)
        {
            return _statuses[connector];
        }

        public async Task SetStatus(Connector connector, ConnectorStatus status, string error = null)
        {
            _statuses[connector] = status;
            await ConnectorStatusChanged?.Invoke(this, new ConnectorEventArgs(connector, status, error));
        }
    }
}
