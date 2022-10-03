namespace Cadenza.Web.Common.Interfaces.Updates;

public interface IUpdatesCoordinator
{
    Task UpdateArtist(ArtistUpdate artist);
    Task UpdateLyrics(TrackUpdate artist);
}