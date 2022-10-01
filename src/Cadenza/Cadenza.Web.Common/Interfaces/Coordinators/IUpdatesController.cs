namespace Cadenza.Web.Common.Interfaces.Coordinators;

public interface IUpdatesController
{
    Task UpdateArtist(ArtistUpdate artist);
    Task UpdateLyrics(TrackUpdate artist);
}