namespace Cadenza.Web.Common.Interfaces.Updates;

public interface IUpdateRepository
{
    Task RemoveTrack(int trackId);
    Task UpdateAlbum(AlbumUpdate albumUpdate);
    Task UpdateArtist(ArtistUpdate artistUpdate);
    Task UpdateTrack(TrackUpdate trackUpdate);
}
