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

        string PlayTracks { get; }
        string PlayArtist { get; }
        string PlayAlbum { get; }

        string SearchArtists { get; }
        string SearchAlbums { get; }
        string SearchTracks { get; }
        string SearchPlaylists { get; }

        string Track { get; }
        string Album { get; }
        string AlbumTracks { get; }
    }
}
