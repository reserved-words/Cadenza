namespace Cadenza.API.Interfaces;

public interface IUpdateService
{
    Task UpdateAlbumTracks(UpdateAlbumTracksDTO request);
    Task UpdateTrack(UpdatedTrackPropertiesDTO updates);
    Task UpdateAlbum(UpdatedAlbumPropertiesDTO updates);
    Task UpdateArtist(UpdatedArtistPropertiesDTO updates);
}
