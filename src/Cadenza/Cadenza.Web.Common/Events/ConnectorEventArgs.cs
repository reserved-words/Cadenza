namespace Cadenza.Web.Common.Events;

public delegate Task ConnectorEventHandler(object sender, ConnectorEventArgs e);

public class ConnectorEventArgs : EventArgs
{
    public ConnectorEventArgs(Connector connector, ConnectorStatus status, string error = null)
    {
        Connector = connector;
        Status = status;
        Error = error;
    }

    public Connector Connector { get; }
    public ConnectorStatus Status { get; }
    public string Error { get; }
}
