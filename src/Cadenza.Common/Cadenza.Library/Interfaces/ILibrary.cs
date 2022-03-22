namespace Cadenza.Library;

public interface ILibrary
{
    Task Populate();
    Task<FullLibrary> Get();
    Task UpdateAlbum(AlbumUpdate update);
    Task UpdateArtist(ArtistUpdate update);
    Task UpdateTrack(TrackUpdate update);
}