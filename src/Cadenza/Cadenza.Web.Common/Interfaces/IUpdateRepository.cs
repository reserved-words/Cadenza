using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.Web.Common.Interfaces;

public interface IUpdateRepository
{
    Task RemoveTrack(int trackId);
    Task UpdateAlbum(AlbumUpdate albumUpdate);
    Task UpdateArtist(ArtistUpdate artistUpdate);
    Task UpdateTrack(TrackUpdate trackUpdate);
}
