namespace Cadenza.Web.Database.Interfaces;

internal interface IDataTransferObjectMapper
{
    UpdateAlbumDTO MapUpdate(AlbumDetailsVM originalAlbum, AlbumDetailsVM updatedAlbum);
    UpdateArtistDTO MapUpdate(ArtistDetailsVM originalArtist, ArtistDetailsVM updatedArtist);
    UpdateTrackDTO MapUpdate(TrackDetailsVM originalTrack, TrackDetailsVM updateTrack);
}
