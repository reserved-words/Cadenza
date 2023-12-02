namespace Cadenza.Web.Api.Interfaces;

internal interface IDataTransferObjectMapper
{
    List<UpdatedAlbumTrackPropertiesDTO> MapAlbumTracks(IReadOnlyCollection<AlbumDiscVM> tracks);
    UpdateAlbumDTO MapUpdate(AlbumDetailsVM originalAlbum, AlbumDetailsVM updatedAlbum);
    UpdateArtistDTO MapUpdate(ArtistDetailsVM originalArtist, ArtistDetailsVM updatedArtist);
    UpdateTrackDTO MapUpdate(TrackDetailsVM originalTrack, TrackDetailsVM updateTrack);
}
