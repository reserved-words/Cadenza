namespace Cadenza.Web.Api.Interfaces;

internal interface IDataTransferObjectMapper
{
    List<UpdatedAlbumTrackPropertiesDTO> MapAlbumTracks(IReadOnlyCollection<AlbumTrackVM> tracks);
    UpdatedAlbumPropertiesDTO MapAlbum(AlbumDetailsVM updatedAlbum);
    UpdatedArtistPropertiesDTO MapArtist(ArtistDetailsVM updatedArtist);
    UpdatedTrackPropertiesDTO MapTrack(TrackDetailsVM updateTrack);
}
