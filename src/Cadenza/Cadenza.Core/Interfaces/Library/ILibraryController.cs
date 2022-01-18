namespace Cadenza.Core;

public interface ILibraryController
{
    Task<bool> UpdateAlbum(AlbumUpdate update);
    Task<bool> UpdateArtist(ArtistUpdate update);
    Task<bool> UpdateTrack(TrackUpdate update);
}