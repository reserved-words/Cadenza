﻿namespace Cadenza.Web.Source.Local.Settings;

public class LocalApiSettings : ApiOptions<LocalApiEndpoints>
{
}

public class LocalApiEndpoints
{
    public string Connect { get; set; }
    public string PlayTrackUrl { get; set; }
}
