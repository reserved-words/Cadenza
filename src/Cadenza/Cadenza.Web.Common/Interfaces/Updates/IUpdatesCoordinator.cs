namespace Cadenza.Web.Common.Interfaces.Updates;

public interface IUpdatesCoordinator
{
    Task RemoveTrack(string trackId);
    Task UpdateAlbum(AlbumUpdate update);
    Task UpdateArtist(ArtistUpdate update);
    Task UpdateTrack(TrackUpdate update);
}