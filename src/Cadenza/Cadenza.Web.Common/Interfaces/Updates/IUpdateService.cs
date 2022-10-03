﻿namespace Cadenza.Web.Common.Interfaces.Updates;

public interface IUpdateService
{
    Task UpdateAlbum(AlbumUpdate albumUpdate);
    Task UpdateArtist(ArtistUpdate artistUpdate);
    Task UpdateTrack(TrackUpdate trackUpdate);
}