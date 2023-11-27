using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.LastFm;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class LastFmMapper : ILastFmMapper
{
    public LovedTrackUpdateDTO MapLovedTrack(GetLovedTrackUpdatesResult data)
    {
        return new LovedTrackUpdateDTO
        {
            TrackId = data.TrackId,
            UserId = data.UserId,
            SessionKey = data.SessionKey,
            IsLoved = data.IsLoved,
            Track = data.Track,
            Artist = data.Artist
        };
    }

    public NowPlayingUpdateDTO MapNowPlaying(GetNowPlayingUpdatesResult data)
    {
        return new NowPlayingUpdateDTO
        {
            UserId = data.UserId,
            SessionKey = data.SessionKey,
            Timestamp = data.Timestamp,
            SecondsRemaining = data.SecondsRemaining,
            Track = data.Track,
            Artist = data.Artist,
            Album = data.Album,
            AlbumArtist = data.AlbumArtist
        };
    }

    public NewScrobbleDTO MapScrobble(GetNewScrobblesResult data)
    {
        return new NewScrobbleDTO
        {
            Id = data.Id,
            SessionKey = data.SessionKey,
            ScrobbledAt = data.ScrobbledAt,
            Track = data.Track,
            Artist = data.Artist,
            Album = data.Album,
            AlbumArtist = data.AlbumArtist
        };
    }
}
