using Cadenza.Web.Common.Model;

namespace Cadenza.Web.Source.Local.Settings;

internal class LocalApiSettings : ApiOptions<LocalApiEndpoints>
{
}

internal class LocalApiEndpoints
{
    public string Connect { get; set; }
    public string ArtworkUrl { get; set; }
    public string PlayTrackUrl { get; set; }
}


