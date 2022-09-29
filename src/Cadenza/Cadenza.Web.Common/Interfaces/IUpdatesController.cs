using Cadenza.Common.Domain.Model.Update;

namespace Cadenza.Web.Common.Interfaces;

public interface IUpdatesController
{
    Task UpdateArtist(ArtistUpdate artist);
    Task UpdateLyrics(TrackUpdate artist);
}