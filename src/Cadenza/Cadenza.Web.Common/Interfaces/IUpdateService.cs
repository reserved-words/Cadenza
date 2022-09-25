using Cadenza.Domain.Models.Update;

namespace Cadenza.Web.Common.Interfaces;

public interface IUpdateService
{
    Task UpdateArtist(ArtistUpdate artistUpdate);
    Task UpdateTrack(TrackUpdate trackUpdate);
}
