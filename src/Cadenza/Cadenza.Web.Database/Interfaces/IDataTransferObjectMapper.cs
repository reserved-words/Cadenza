﻿namespace Cadenza.Web.Database.Interfaces;

internal interface IDataTransferObjectMapper
{
    List<AlbumTrackDTO> MapAlbumTracks(IReadOnlyCollection<AlbumDiscVM> tracks);
    UpdateAlbumDTO MapUpdate(AlbumDetailsVM originalAlbum, AlbumDetailsVM updatedAlbum);
    UpdateArtistDTO MapUpdate(ArtistDetailsVM originalArtist, ArtistDetailsVM updatedArtist);
    UpdateTrackDTO MapUpdate(TrackDetailsVM originalTrack, TrackDetailsVM updateTrack);
}