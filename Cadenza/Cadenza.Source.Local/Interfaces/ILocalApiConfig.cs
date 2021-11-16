namespace Cadenza.Source.Local;

public interface ILocalApiConfig
{
    string AlbumArtistsUrl { get; }
    string ArtistUrl { get; }
    string PlaylistAllUrl { get; }
    string QueuedUpdatesUrl { get; }
    string TrackSummaryUrl { get; }
    string TrackUrl { get; }
    string TrackUriFormat { get; }
    string UpdateAlbumUrl { get; }
    string UpdateArtistUrl { get; }
    string UpdateTrackUrl { get; }
    string UnqueueUrl { get; }
}
