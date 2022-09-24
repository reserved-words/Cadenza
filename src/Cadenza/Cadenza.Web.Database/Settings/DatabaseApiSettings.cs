using Cadenza.Web.Common.Model;

namespace Cadenza.Web.Database.Settings;

internal class DatabaseApiSettings : ApiOptions<LocalApiEndpoints>
{
}

internal class LocalApiEndpoints
{
    public string Connect { get; set; }
    public string PlayTrackUrl { get; set; }

    public string Populate { get; set; }

    public string GetUpdates { get; set; }
    public string UpdateAlbum { get; set; }
    public string UpdateArtist { get; set; }
    public string UpdateTrack { get; set; }
    public string UnqueueUpdate { get; set; }

    public string Artist { get; set; }
    public string ArtistAlbums { get; set; }
    public string AllArtists { get; set; }
    public string AlbumArtists { get; set; }
    public string TrackArtists { get; set; }
    public string GroupingArtists { get; set; }
    public string GenreArtists { get; set; }

    public string PlayTracks { get; set; }
    public string PlayArtist { get; set; }
    public string PlayAlbum { get; set; }
    public string PlayGenre { get; set; }
    public string PlayGrouping { get; set; }

    public string SearchArtists { get; set; }
    public string SearchAlbums { get; set; }
    public string SearchTracks { get; set; }
    public string SearchPlaylists { get; set; }
    public string SearchGenres { get; set; }
    public string SearchGroupings { get; set; }

    public string Track { get; set; }
    public string Album { get; set; }
    public string AlbumTracks { get; set; }

    public string AddSource { get; set; }
}


