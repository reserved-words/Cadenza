namespace Cadenza.Web.Api.Interfaces;

internal interface IDataTransferObjectMapper
{
    List<UpdatedAlbumTrackPropertiesDTO> MapAlbumTracks(IReadOnlyCollection<AlbumDiscVM> tracks);
    UpdatedAlbumPropertiesDTO MapUpdate(AlbumDetailsVM updatedAlbum);
    UpdatedArtistPropertiesDTO MapUpdate(ArtistDetailsVM updatedArtist);
    UpdatedTrackPropertiesDTO MapUpdate(TrackDetailsVM updateTrack);
}
