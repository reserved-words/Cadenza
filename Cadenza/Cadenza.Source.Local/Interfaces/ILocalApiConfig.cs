namespace Cadenza.Source.Local;

public interface ILocalApiConfig
{
    string BaseUrl { get; }

    string AllTracksUrl { get; }
    string AlbumTracksUrl { get; }
    string ArtistTracksUrl { get; }

    string AlbumArtistsUrl { get; }
    string AlbumsUrl { get; }
    string AlbumTrackLinksUrl { get; }
    string ArtistUrl { get; }
    string ArtistsUrl { get; }
    string QueuedUpdatesUrl { get; }
    string TrackUrl { get; }
    string FullTrackUrl { get; }
    string TrackUriFormat { get; }
    string UpdateAlbumUrl { get; }
    string UpdateArtistUrl { get; }
    string UpdateTrackUrl { get; }
    string UnqueueUrl { get; }
}
