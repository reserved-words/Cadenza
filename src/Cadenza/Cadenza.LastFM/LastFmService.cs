using Cadenza.API.Core.LastFM;
using Cadenza.Common;
using Cadenza.Core;
using Cadenza.Domain;
using Track = Cadenza.API.Core.LastFM.Track;

namespace Cadenza.LastFM;

public class LastFmService : IPlayTracker, IFavouritesConsumer, IFavouritesController
{
    private readonly IStoreGetter _store;
    private readonly IFavourites _favourites;
    private readonly IScrobbler _scrobbler;

    public LastFmService(IStoreGetter store, IFavourites favourites, IScrobbler scrobbler)
    {
        _store = store;
        _favourites = favourites;
        _scrobbler = scrobbler;
    }

    public async Task RecordPlay(TrackSummary track, DateTime timestamp)
    {
        var model = await GetScrobble(track, null, timestamp);
        await _scrobbler.RecordPlay(model);
    }

    public async Task UpdateNowPlaying(TrackSummary track, int duration)
    {
        var model = await GetScrobble(track, duration, null);
        await _scrobbler.UpdateNowPlaying(model);
    }

    public async Task<bool> IsFavourite(string artist, string title)
    {
        return await _favourites.IsFavourite(artist, title);
    }

    public async Task Favourite(string artist, string title)
    {
        var track = await GetTrack(artist, title);
        await _favourites.Favourite(track);
    }

    public async Task Unfavourite(string artist, string title)
    {
        var track = await GetTrack(artist, title);
        await _favourites.Unfavourite(track);
    }

    public async Task<Track> GetTrack(string artist, string title)
    {
        var storedSessionKey = await _store.GetValue<string>(StoreKey.LastFmSessionKey);

        return new Track
        {
            SessionKey = storedSessionKey.Value,
            Artist = artist,
            Title = title
        };
    }

    public async Task<Scrobble> GetScrobble(TrackSummary track, int? duration, DateTime? timestamp)
    {
        var sessionKey = await _store.GetValue<string>(StoreKey.LastFmSessionKey);

        // Might be a better way to do this in future but for now omit album details for Spotify playlists
        return track.ReleaseType == ReleaseType.Playlist
            ? new Scrobble
            {
                SessionKey = sessionKey.Value,
                Timestamp = timestamp ?? DateTime.Now,
                Artist = track.Artist,
                Title = track.Title,
                Duration = duration ?? track.DurationSeconds
            }
            : new Scrobble
            {
                SessionKey = sessionKey.Value,
                Timestamp = timestamp ?? DateTime.Now,
                Artist = track.Artist,
                Title = track.Title,
                AlbumTitle = track.AlbumTitle,
                AlbumArtist = track.AlbumArtist,
                Duration = duration ?? track.DurationSeconds
            };
    }
}
