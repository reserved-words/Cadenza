namespace Cadenza.Web.Common.Interfaces.Updates;

public interface IUpdatesMessenger
{
    event ArtistUpdatedEventHandler ArtistUpdated;
    event LyricsUpdatedEventHandler LyricsUpdated;
}
