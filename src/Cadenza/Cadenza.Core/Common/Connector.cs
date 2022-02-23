namespace Cadenza.Core.Common
{
    public enum Connector
    {
        API,
        LastFm,
        Local,
        Spotify
    }

    public enum ConnectorStatus
    {
        Loading,
        Connected,
        Disabled,
        Errored
    }
}
