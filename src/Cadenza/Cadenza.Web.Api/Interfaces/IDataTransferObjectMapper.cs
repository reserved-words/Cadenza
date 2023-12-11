namespace Cadenza.Web.Api.Interfaces;

internal interface IDataTransferObjectMapper
{
    UpdatedAlbumPropertiesDTO MapAlbum(AlbumDetailsVM album);
    List<UpdatedAlbumTrackPropertiesDTO> MapAlbumTracks(IReadOnlyCollection<AlbumTrackVM> tracks);
    UpdatedArtistPropertiesDTO MapArtist(ArtistDetailsVM artist);
    List<UpdatedArtistReleasePropertiesDTO> MapArtistReleases(IReadOnlyCollection<AlbumVM> releases);
    UpdatedTrackPropertiesDTO MapTrack(TrackDetailsVM track);
}
