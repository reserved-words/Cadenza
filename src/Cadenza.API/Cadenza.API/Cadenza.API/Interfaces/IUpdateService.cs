namespace Cadenza.API.Interfaces;

public interface IUpdateService
{
    Task UpdateTrack(UpdatedTrackPropertiesDTO updates);
    Task UpdateAlbum(AlbumUpdateDTO request);
    Task UpdateArtist(UpdatedArtistPropertiesDTO updates);
}
