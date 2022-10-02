namespace Cadenza.Web.Common.Interfaces.Connections;

public interface IConnectionMessenger
{
    event ConnectorEventHandler ConnectorStatusChanged;
}
