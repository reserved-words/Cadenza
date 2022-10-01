using Cadenza.Web.Common.Interfaces.Coordinators;

namespace Cadenza.Web.Core.Coordinators;

internal class ConnectionCoordinator : IConnectorConsumer, IConnectorController
{
    private readonly Dictionary<Connector, ConnectorStatus> _statuses;

    public ConnectionCoordinator()
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
