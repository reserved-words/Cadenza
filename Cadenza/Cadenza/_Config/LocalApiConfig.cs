using Cadenza.Source.Local;

namespace Cadenza._Config;

public class LocalApiConfig : ILocalApiConfig
{
    private readonly IConfiguration _config;

    public LocalApiConfig(IConfiguration config)
    {
        _config = config;
    }

    private const string Placeholder = "{0}";

    public string BaseUrl => _config.GetSection("LocalApi").GetValue<string>("BaseUrl");

    public string AlbumsUrl => $"{BaseUrl}/Library/Albums";
    public string ArtistsUrl => $"{BaseUrl}/Library/Artists";
    public string TrackUrl => $"{BaseUrl}/Library/Track/{Placeholder}";
    public string FullTrackUrl => $"{BaseUrl}/Library/FullTrack/{Placeholder}";

    public string TrackUriFormat => $"{BaseUrl}/Play/Track/{Placeholder}";

    public string UpdateAlbumUrl => $"{BaseUrl}/Update/Album";
    public string UpdateArtistUrl => $"{BaseUrl}/Update/Artist";
    public string UpdateTrackUrl => $"{BaseUrl}/Update/Track";
    public string QueuedUpdatesUrl => $"{BaseUrl}/Update/Queue";
    public string UnqueueUrl => $"{BaseUrl}/Update/Unqueue";

    public string AlbumTracksUrl => $"{BaseUrl}/Library/AlbumTracks/{Placeholder}";
    public string ArtistTracksUrl => $"{BaseUrl}/Library/ArtistTracks/{Placeholder}";
    public string AllTracksUrl => $"{BaseUrl}/Library/AllTracks";
}
