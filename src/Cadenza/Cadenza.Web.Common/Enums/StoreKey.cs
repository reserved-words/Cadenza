namespace Cadenza.Web.Common.Enums;

public enum StoreKey
{
    LastFmSessionKey, // infinite lifespan - but can be revoked
    LastFmToken, // 60 minute lifespan
    CurrentTrack,
    CurrentTrackId,
    CurrentTrackSource
}