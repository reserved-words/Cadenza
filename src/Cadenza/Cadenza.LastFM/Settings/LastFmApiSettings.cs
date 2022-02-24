using Cadenza.Core;

namespace Cadenza.LastFM;

internal class LastFmApiSettings : ApiOptions<LastFmApiEndpoints>
{
    public string RedirectUri { get; set; }
}

internal class LastFmApiEndpoints
{
    public string AuthUrl { get; set; }
    public string CreateSession { get; set; }
    public string Unfavourite { get; set; }
    public string IsFavourite { get; set; }
    public string Favourite { get; set; }
    public string TopAlbums { get; set; }
    public string TopArtists { get; set; }
    public string TopTracks { get; set; }
    public string RecentTracks { get; set; }
    public string UpdateNowPlaying { get; set; }
    public string Scrobble { get; set; }
}


