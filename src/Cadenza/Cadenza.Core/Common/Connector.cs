namespace Cadenza.Core.Common
{
    public enum Connector
    {
        API,
        LastFm,
        Local
    }

    public enum ConnectorStatus
    {
        Loading,
        Connected,
        Disabled,
        Errored
    }
}
