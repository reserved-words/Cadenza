namespace Cadenza.Core;

public delegate Task ConnectorEventHandler(object sender, ConnectorEventArgs e);

public class ConnectorEventArgs : EventArgs
{
    public ConnectorEventArgs(Connector source, string error)
    {
        Connector = source;
        Error = error;
    }

    public Connector Connector { get; set; }
    public string Error { get; set; }
}