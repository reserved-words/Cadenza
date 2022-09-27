using Cadenza.Domain.Model.Update;

namespace Cadenza.Web.Core.Interfaces;

public interface IUpdatesController
{
    Task UpdateArtist(ArtistUpdate artist);
    Task UpdateLyrics(TrackUpdate artist);
}