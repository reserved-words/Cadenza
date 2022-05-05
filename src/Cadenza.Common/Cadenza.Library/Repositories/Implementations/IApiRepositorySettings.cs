namespace Cadenza.Library.Repositories
{
    public interface IApiRepositorySettings
    {
        string BaseUrl { get; }

        string Artist { get; }
        string ArtistAlbums { get; }
        string AllArtists { get; }
        string AlbumArtists { get; }
        string TrackArtists { get; }
        string ArtistsByGrouping { get; }
        string ArtistsByGenre { get; }

        string PlayTracks { get; }
        string PlayArtist { get; }
        string PlayAlbum { get; }
        string PlayGrouping { get; }
        string PlayGenre { get; }

        string SearchArtists { get; }
        string SearchAlbums { get; }
        string SearchTracks { get; }
        string SearchPlaylists { get; }
        string SearchGenres { get; }
        string SearchGroupings { get; }

        string Track { get; }
        string Album { get; }
        string AlbumTracks { get; }

        string UpdateAlbum { get; }
        string UpdateArtist { get; }
        string UpdateTrack { get; }
    }
}
