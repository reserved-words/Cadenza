using Cadenza.Core;

namespace Cadenza.Source.Spotify.Settings;

internal class SpotifyApiSettings : ApiOptions<SpotifyApiEndpoints>
{
    public string RedirectUri { get; set; }
}

internal class SpotifyApiEndpoints
{
    public string Populate { get; set; }
    public string AuthUrl { get; set; }
    public string TokenUrl { get; set; }
    public string AuthHeader { get; set; }

    public string QueuedUpdates { get; set; }
    public string PlayTrackUrl { get; set; }
    public string UpdateAlbum { get; set; }
    public string UpdateArtist { get; set; }
    public string UpdateTrack { get; set; }
    public string UnqueueUpdate { get; set; }

    public string Artist { get; set; }
    public string ArtistAlbums { get; set; }
    public string AllArtists { get; set; }
    public string AlbumArtists { get; set; }
    public string TrackArtists { get; set; }

    public string PlayTracks { get; set; }
    public string PlayArtist { get; set; }
    public string PlayAlbum { get; set; }

    public string SearchArtists { get; set; }
    public string SearchAlbums { get; set; }
    public string SearchTracks { get; set; }
    public string SearchPlaylists { get; set; }

    public string Track { get; set; }
    public string Album { get; set; }
    public string AlbumTracks { get; set; }
}