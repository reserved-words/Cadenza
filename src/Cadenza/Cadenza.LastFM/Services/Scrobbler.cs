using Cadenza.Core;
using Cadenza.Core.App;
using Cadenza.Core.Interfaces;
using Cadenza.Domain;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;

namespace Cadenza.LastFM;

internal class Scrobbler : IPlayTracker
{
    private readonly IHttpHelper _http;
    private readonly IUrl _url;
    private readonly IStoreGetter _store;
    private readonly LastFmApiSettings _apiSettings;

    public Scrobbler(IUrl url, IHttpHelper http, IOptions<LastFmApiSettings> settings, IStoreGetter store)
    {
        _url = url;
        _http = http;
        _apiSettings = settings.Value;
        _store = store;
    }

    public async Task RecordPlay(TrackFull track, DateTime timestamp)
    {
        var scrobble = await GetScrobble(track, null, timestamp);
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.Scrobble);
        await _http.Post(url, null, scrobble);
    }

    public async Task UpdateNowPlaying(TrackFull track, int duration)
    {
        var scrobble = await GetScrobble(track, duration, null);
        var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.UpdateNowPlaying);
        await _http.Post(url, null, scrobble);
    }

    private async Task<Scrobble> GetScrobble(TrackFull track, int? duration, DateTime? timestamp)
    {
        var sessionKey = await _store.GetValue<string>(StoreKey.LastFmSessionKey);

        // Might be a better way to do this in future but for now omit album details for Spotify playlists
        return track.Album.ReleaseType == ReleaseType.Playlist
            ? new Scrobble
            {
                SessionKey = sessionKey.Value,
                Timestamp = timestamp ?? DateTime.Now,
                Artist = track.Artist.Name,
                Title = track.Track.Title,
                Duration = duration ?? track.Track.DurationSeconds
            }
            : new Scrobble
            {
                SessionKey = sessionKey.Value,
                Timestamp = timestamp ?? DateTime.Now,
                Artist = track.Artist.Name,
                Title = track.Track.Title,
                AlbumTitle = track.Album.Title,
                AlbumArtist = track.Album.ArtistName,
                Duration = duration ?? track.Track.DurationSeconds
            };
    }
}
