using Cadenza.Domain.Models.Update;

namespace Cadenza.Core.App;

public interface IUpdatesController
{
    Task UpdateArtist(ArtistUpdate artist);
    Task UpdateLyrics(TrackUpdate artist);
}