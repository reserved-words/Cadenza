using Cadenza.Web.Common.Enums;

namespace Cadenza.Web.Core.Interfaces
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
}
