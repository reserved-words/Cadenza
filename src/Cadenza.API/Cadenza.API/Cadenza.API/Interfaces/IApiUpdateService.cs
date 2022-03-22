namespace Cadenza.API.Interfaces;

public interface IApiUpdateService
{
    Task UpdateAlbum(AlbumUpdate update);
    Task UpdateArtist(ArtistUpdate update);
    Task UpdateTrack(TrackUpdate update);
}
