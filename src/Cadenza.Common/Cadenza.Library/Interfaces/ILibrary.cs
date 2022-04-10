namespace Cadenza.Library;

public interface ILibrary
{
    Task<FullLibrary> Get();
    Task UpdateArtist(ArtistUpdate update);
    Task UpdateAlbum(AlbumUpdate update);
    Task UpdateTrack(TrackUpdate update);
}