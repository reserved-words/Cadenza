using Cadenza.Web.Common.Interfaces.Connections;

namespace Cadenza.Web.Core.Coordinators;

internal class ConnectionCoordinator : IConnectionCoordinator, IConnectionService
{
    private readonly Dictionary<Connector, ConnectorStatus> _statuses;
    private readonly IMessenger _messenger;

    public ConnectionCoordinator(IMessenger messenger)
    {
        _statuses = Enum.GetValues<Connector>()
            .ToDictionary(c => c, c => ConnectorStatus.Loading);
        _messenger = messenger;
    }

    public ConnectorStatus GetStatus(Connector connector)
    {
        return _statuses[connector];
    }

    public async Task SetStatus(Connector connector, ConnectorStatus status, string error = null)
    {
        _statuses[connector] = status;
        await _messenger.Send(this, new ConnectorEventArgs(connector, status, error));
    }
}
