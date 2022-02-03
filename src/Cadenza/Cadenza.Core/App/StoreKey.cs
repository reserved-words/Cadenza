﻿namespace Cadenza.Core;

public enum StoreKey
{
    SpotifyAccessToken,
    SpotifyCode,
    SpotifyDeviceId,
    SpotifyRefreshToken,
    LastFmSessionKey, // infinite lifespan - but can be revoked
    LastFmToken, // 60 minute lifespan
    CurrentTrack,
    CurrentTrackSource
}