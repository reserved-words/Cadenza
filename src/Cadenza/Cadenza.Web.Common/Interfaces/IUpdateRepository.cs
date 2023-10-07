namespace Cadenza.Web.Common.Interfaces;

public interface IUpdateRepository
{
    Task RemoveTrack(int trackId);
    Task UpdateAlbum(AlbumUpdateVM albumUpdate);
    Task UpdateArtist(ArtistUpdateVM artistUpdate);
    Task UpdateTrack(TrackUpdateVM trackUpdate);
}
