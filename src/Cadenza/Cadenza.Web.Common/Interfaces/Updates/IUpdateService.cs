﻿namespace Cadenza.Web.Common.Interfaces.Updates;

public interface IUpdateService
{
    Task RemoveTrack(int trackId);
    Task UpdateAlbum(AlbumUpdate albumUpdate);
    Task UpdateArtist(ArtistUpdate artistUpdate);
    Task UpdateTrack(TrackUpdate trackUpdate);
}
