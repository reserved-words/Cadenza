﻿namespace Cadenza.Core
{
    public enum Connector
    {
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
