namespace Cadenza.Core.App;

public enum StoreKey
{
    SpotifyAccessToken,
    SpotifyCode,
    SpotifyState,
    SpotifyDeviceId,
    SpotifyRefreshToken,
    LastFmSessionKey, // infinite lifespan - but can be revoked
    LastFmToken, // 60 minute lifespan
    CurrentTrack,
    CurrentTrackSource
}