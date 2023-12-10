namespace Cadenza.Web.Api.Interfaces;

internal interface IDataTransferObjectMapper
{
    List<UpdatedAlbumTrackPropertiesDTO> MapAlbumTracks(IReadOnlyCollection<UpdateAlbumTrackVM> tracks);
    UpdatedAlbumPropertiesDTO MapAlbum(UpdateAlbumVM updatedAlbum);
    UpdatedArtistPropertiesDTO MapArtist(ArtistDetailsVM updatedArtist);
    UpdatedTrackPropertiesDTO MapTrack(TrackDetailsVM updateTrack);
    UpdateAlbumVM MapAlbum(AlbumForUpdateDTO album);
    UpdateAlbumTrackVM MapTrack(AlbumTrackForUpdateDTO track);
}
