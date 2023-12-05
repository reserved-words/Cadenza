﻿namespace Cadenza.Web.Api.Interfaces;

public interface IUpdateApi
{
    Task UpdateAlbumTracks(int albumId, IReadOnlyCollection<AlbumDiscVM> originalTracks, IReadOnlyCollection<AlbumDiscVM> updatedTracks);
    Task UpdateAlbum(AlbumDetailsVM originalAlbum, AlbumDetailsVM updatedAlbum);
    Task UpdateArtist(ArtistDetailsVM originalArtist, ArtistDetailsVM updatedArtist);
    Task UpdateTrack(TrackDetailsVM originalTrack, TrackDetailsVM updatedTrack);
}