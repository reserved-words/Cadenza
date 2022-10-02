namespace Cadenza.Web.Common.Interfaces.Updates;

public interface IUpdatesController
{
    Task UpdateArtist(ArtistUpdate artist);
    Task UpdateLyrics(TrackUpdate artist);
}