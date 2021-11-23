using Cadenza.Source.Local;

namespace Cadenza._Config;

public class LocalApiConfig : ILocalApiConfig
{
    private const string BaseUrl = "http://localhost:19213"; // put in app settings
    private const string Placeholder = "{0}";

    public string AlbumArtistsUrl => $"{BaseUrl}/Library/AlbumArtists";
    public string AlbumsUrl => $"{BaseUrl}/Library/Albums";
    public string AlbumTrackLinksUrl => $"{BaseUrl}/Library/AlbumTrackLinks";
    public string ArtistUrl => $"{BaseUrl}/Library/Artist/{Placeholder}";
    public string ArtistsUrl => $"{BaseUrl}/Library/Artists";
    public string TrackSummaryUrl => $"{BaseUrl}/Library/TrackSummary/{Placeholder}";
    public string TrackUrl => $"{BaseUrl}/Library/Track/{Placeholder}";
    public string TracksUrl => $"{BaseUrl}/Library/Tracks";

    public string TrackUriFormat => $"{BaseUrl}/Play/Track/{Placeholder}";

    public string PlaylistAllUrl => $"{BaseUrl}/Playlist/All";

    public string UpdateAlbumUrl => $"{BaseUrl}/Update/Album";
    public string UpdateArtistUrl => $"{BaseUrl}/Update/Artist";
    public string UpdateTrackUrl => $"{BaseUrl}/Update/Track";
    public string QueuedUpdatesUrl => $"{BaseUrl}/Update/Queue";
    public string UnqueueUrl => $"{BaseUrl}/Update/Unqueue";
}
