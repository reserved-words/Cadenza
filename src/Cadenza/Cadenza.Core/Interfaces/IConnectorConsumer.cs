namespace Cadenza.Core
{
    public delegate Task ConnectorEventHandler(object sender, ConnectorEventArgs e);

    public class ConnectorEventArgs : EventArgs
    {
        public ConnectorEventArgs(Connector connector, ConnectorStatus status, string error = null)
        {
            Connector = connector;
            Status = status;
            Error = null;
        }

        public Connector Connector { get; }
        public ConnectorStatus Status { get; }
        public string Error { get; }
    }

    public interface IConnectorConsumer
    {
        ConnectorStatus GetStatus(Connector connector);
        event ConnectorEventHandler ConnectorStatusChanged;
    }

    public interface IConnectorController
    {
        Task SetStatus(Connector connector, ConnectorStatus status, string error = null);
    }

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
