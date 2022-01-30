using Cadenza.Domain;

namespace Cadenza.Core;

public class ConnectorException : Exception
{
    public ConnectorException(Connector connector, ConnectorError error, string message)
        : base(message)
    {
        Connector = connector;
        Error = error;
    }

    public Connector Connector { get; }
    public ConnectorError Error { get; }
}

public enum ConnectorError
{
    ConnectFailure,
    PlaybackFailure
}
