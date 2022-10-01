namespace Cadenza.Web.Common.Interfaces.Coordinators;

public interface IUpdatesConsumer
{
    event ArtistUpdatedEventHandler ArtistUpdated;
    event LyricsUpdatedEventHandler LyricsUpdated;
}
