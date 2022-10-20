using Cadenza.Web.Common.Model;

namespace Cadenza.Web.Info.Settings;

public class InfoApiSettings : ApiOptions<InfoApiEndpoints>
{
}

public class InfoApiEndpoints
{
    public string AlbumArtworkUrl { get; set; }
    public string ArtistImageUrl { get; set; }
}


